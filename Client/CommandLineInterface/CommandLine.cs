using System.Runtime.CompilerServices;
using Spectre.Console;

namespace Client.CommandLineInterface;

public static class CommandLine
{
    public static string AskAQuestion(Action<Question> questionConfigurator)
    {
        var question = new Question();
        questionConfigurator(question);

        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(question.QuestionText)
                .PageSize(10)
                .HighlightStyle(new Style(Color.Yellow))
                .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
                .AddChoices(question.Choices));
    }
}