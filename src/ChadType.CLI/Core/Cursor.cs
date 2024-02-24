namespace ChadType.CLI.Core;

public class Cursor
{
    public TypingTest Test { get; }
    public int Word { get; set; }
    public int Character { get; set; }

    public ref TyperWord CurrentWord => ref Test.Words[Word];
    public ref TyperChar CurrentChar => ref CurrentWord.Chars[Character];

    public bool IsWordCompleted => Character >= CurrentWord.Length;

    public Cursor(TypingTest test)
    {
        Test = test;
    }

    public void Advance()
    {
        Character++;
    }

    public void AdvanceWord()
    {
        Word++;
        Character = 0;
    }

    public void Backtrack()
    {
        /*
         * if we are at the start of the current word => return to the absolute end of last word (including extra chars)
         * otherwise => just backtrack single char
         */
        if (Character == 0)
        {
            Word--;
            Character = CurrentWord.FullLength;
        }
        else
        {
            Character--;
        }
    }

    public void BacktrackWord()
    {
        /*
         * if current char is 0 => backtrack to start of the last word
         * otherwise => backtrack to start of the current word
         */
        if (Character == 0)
        {
            Word--;
            Character = 0;
        }
        else
        {
            Character = 0;
        }
    }
}
