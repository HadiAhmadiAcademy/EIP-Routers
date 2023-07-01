namespace Client.Factories;

public static class ListExtensions
{
    public static List<T> Shuffle<T>(this List<T> input)
    {
        return input.OrderBy(a => Guid.NewGuid()).ToList();
    }
}