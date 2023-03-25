namespace Client.CommandLineInterface;

public static class ChoiceParser
{
    public static long GetIndexOfSelectedChoice(this string input)
    {
        if (string.IsNullOrEmpty(input)) return Choices.Unknown;

        var index = input.IndexOf('.');
        input = input.Substring(0, index);

        return long.TryParse(input, out var choice) ? choice : Choices.Unknown;
    }
}