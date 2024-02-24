namespace ChadType.CLI.Core;

public static class ConsoleKeyInfoExtensions
{
    public static bool IsBackspace(this ConsoleKeyInfo self) => self.Key == ConsoleKey.Backspace;

    public static bool IsCtrlBackspace(this ConsoleKeyInfo self) =>
        self.IsBackspace() && (self.Modifiers & ConsoleModifiers.Control) != 0;

    public static bool IsSpacebar(this ConsoleKeyInfo self) => self.Key == ConsoleKey.Spacebar;

    public static bool IsValidInputChar(this ConsoleKeyInfo self) => self switch
    {
        { KeyChar: var c } when char.IsLetterOrDigit(c) || char.IsPunctuation(c) => true,
        _ => false
    };
}