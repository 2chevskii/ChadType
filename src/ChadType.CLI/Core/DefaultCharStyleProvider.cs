using Spectre.Console;

namespace ChadType.CLI.Core;

public class DefaultCharStyleProvider : ICharStyleProvider
{
    /// <summary>
    /// Character is awaiting to be typed
    /// </summary>
    public Style Pending => new Style(Color.Grey35);

    /// <summary>
    /// Character is awaiting to be typed and the cursor in on it's word
    /// </summary>
    public Style PendingCurrentWord => new Style(Color.Yellow);

    /// <summary>
    /// Character is awaiting to be typed and the cursor is on it
    /// </summary>
    public Style PendingCurrentChar => new Style(Color.Yellow, decoration: Decoration.Underline);

    /// <summary>
    /// Character was skipped (cursor is on next word)
    /// </summary>
    public Style PendingLeftout => new Style(Color.Red3, decoration: Decoration.Underline);

    /// <summary>
    /// Character was typed correctly
    /// </summary>
    public Style Valid => new Style(Color.White);

    /// <summary>
    /// Character was typed invalid
    /// </summary>
    public Style Invalid => new Style(Color.Red3, decoration: Decoration.Underline);

    /// <summary>
    /// Character does not belong in this word
    /// </summary>
    public Style Extra => new Style(Color.Red3);
}
