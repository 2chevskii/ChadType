namespace ChadType.CLI.Core;

public struct TyperChar
{
    public TyperCharState State { get; set; }
    public char Value { get; }

    public TyperChar(TyperCharState state, char value)
    {
        State = state;
        Value = value;
    }
}
