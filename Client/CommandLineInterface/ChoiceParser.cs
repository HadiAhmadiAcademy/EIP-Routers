namespace Client.CommandLineInterface;

public static class ChoiceParser
{
    public static long GetIndexOfSelectedChoice(this string input)
    {
        if (string.IsNullOrEmpty(input)) return Choices.Unknown;

        var firstCharacter = input[0];
        return long.TryParse(input, out var index) ? index : Choices.Unknown;
    }
}