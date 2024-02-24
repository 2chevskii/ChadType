using Spectre.Console;

namespace ChadType.CLI.Core;

public unsafe class TypingTest
{
    private readonly ICharStyleProvider _charStyleProvider;
    public TyperWord[] Words { get; }
    public Cursor Cursor { get; }

    public TypingTest(TyperWord[] words, ICharStyleProvider charStyleProvider)
    {
        _charStyleProvider = charStyleProvider;
        Words = words;
        Cursor = new Cursor(this);
    }

    public void HandleInput(ConsoleKeyInfo keyInfo)
    {
        if (keyInfo.IsCtrlBackspace())
        {
        }
        else if (keyInfo.IsBackspace())
        {
            Cursor.Backtrack();
            if (Cursor.Character < Cursor.CurrentWord.Length)
            {
                ref var c = ref Cursor.CurrentChar;
                c.State = TyperCharState.Pending;
            }
            else if (Cursor.Character < Cursor.CurrentWord.FullLength)
            {
                ref var w = ref Cursor.CurrentWord;
                w.ExtraChars.RemoveAt(w.ExtraLength - 1);
            }
        }
        else if (keyInfo.IsSpacebar())
        {
            Cursor.AdvanceWord();
        }
        else if (keyInfo.IsValidInputChar())
        {
            ref var word = ref Cursor.CurrentWord;
            if (Cursor.IsWordCompleted)
            {
                word.ExtraChars.Add(keyInfo.KeyChar);
            }
            else
            {
                ref var character = ref Cursor.CurrentChar;

                character.State = character.Value.Equals(keyInfo.KeyChar)
                    ? TyperCharState.Valid
                    : TyperCharState.Invalid;
            }

            Cursor.Advance();
        }
    }

    public Paragraph Render()
    {
        Paragraph paragraph = new Paragraph();

        for (var i = 0; i < Words.Length; i++)
        {
            if (i != 0)
            {
                paragraph.Append(" ");
            }

            ref var word = ref Words[i];

            for (var j = 0; j < word.Chars.Length; j++)
            {
                ref var character = ref word.Chars[j];

                Style style;

                switch (character.State)
                {
                    case TyperCharState.Pending:
                        style = _charStyleProvider.Pending;

                        if (i == Cursor.Word)
                        {
                            style = new Style(Color.Yellow);
                            if (j == Cursor.Character)
                            {
                                style = style.Decoration(Decoration.Underline);
                            }
                        }

                        break;
                    case TyperCharState.Valid:
                        style = _charStyleProvider.Valid;
                        break;
                    case TyperCharState.Invalid:
                        style = _charStyleProvider.Invalid;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                paragraph.Append(character.Value.ToString(), style);
            }

            for (var j = 0; j < word.ExtraChars.Count; j++)
            {
                var character = word.ExtraChars[j];

                Style style = _charStyleProvider.Extra;

                paragraph.Append(character.ToString(), style);
            }
        }

        return paragraph;
    }
}
