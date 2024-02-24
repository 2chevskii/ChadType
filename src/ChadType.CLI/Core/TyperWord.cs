namespace ChadType.CLI.Core;

public struct TyperWord
{
    public TyperChar[] Chars { get; }
    public List<char> ExtraChars { get; }
    public int Length => Chars.Length;
    public int ExtraLength => ExtraChars.Count;
    public int FullLength => Length + ExtraLength;

    public TyperWord(TyperChar[] chars)
    {
        Chars = chars;
        ExtraChars = new List<char>();
    }
}
