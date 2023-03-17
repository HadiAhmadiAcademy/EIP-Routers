namespace Client.CommandLineInterface;

public class Question
{
    public string QuestionText { get; set; }
    public List<string> Choices { get; set; }

    public Question() { }

    public Question About(string question)
    {
        QuestionText = question;
        return this;
    }

    public Question WithChoices(params string[] choices)
    {
        this.Choices = choices.ToList();
        return this;
    }
}