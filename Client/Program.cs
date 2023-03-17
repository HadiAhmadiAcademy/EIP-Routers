using Client.CommandLineInterface;
using Spectre.Console;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var choice = CommandLine.AskAQuestion(a=> 
                    a.About("Select the next action:")
                        .WithChoices(
                            "1.Send 'Order' Message",
                            "2.Dispatch 'PlaceOrder' Command",
                            "3.Publish 'OrderPlaced' Event",
                            "6.Exit"
                        )).GetIndexOfSelectedChoice();

            }
        }
    }
}