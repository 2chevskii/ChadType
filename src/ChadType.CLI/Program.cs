using ChadType.CLI.Core;
using Spectre.Console;

const string testString = "A quick brown fox jumps over a lazy dog";

IEnumerable<TyperWord> testWords = testString
    .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
    .Select(splt => new TyperWord(splt.Select(c => new TyperChar(TyperCharState.Pending, c)).ToArray()));

var test = new TypingTest(testWords.ToArray(), new DefaultCharStyleProvider());
AnsiConsole.Clear();

var rootLayout = new Layout("root")
    .SplitRows(
        new Layout("stats"),
        new Layout("prompt").Ratio(3)
    );


AnsiConsole.Live(rootLayout)
    .Start(context =>
    {
        while (true)
        {
            rootLayout.GetLayout("prompt").Update(new Panel(
                test.Render()
            ).Expand());

            rootLayout.GetLayout("stats")
                .Update(new Panel(
                    new Rows(
                        new Text("CHAR: " + test.Cursor.Character),
                        new Text("WORD: " + test.Cursor.Word)
                    )
                ).Expand());

            context.Refresh();
            test.HandleInput(Console.ReadKey(true));
        }
    });
