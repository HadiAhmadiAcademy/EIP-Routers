using CommandLine.Text;
using CommandLine;
using Newtonsoft.Json;

namespace Router.Core;

public class RouterArguments
{
    [Option('i', "input", Required = false, HelpText = "Set input queue for the router", Default = "Resequencer")]
    public string InputQueue { get; set; }

    [Option('o', "output", Required = false, HelpText = "Set output queue for the router", Default = "Consumer1")]
    public string OutputQueue { get; set; }
}
