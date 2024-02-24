using Spectre.Console;

namespace ChadType.CLI.Core;

public interface ICharStyleProvider
{
    Style Pending { get; }
    Style PendingCurrentWord { get; }
    Style PendingCurrentChar { get; }
    Style Valid { get; }
    Style Invalid { get; }
    Style Extra { get; }
}
