using CommandLine.Text;
using CommandLine;
using Newtonsoft.Json;

namespace Router.Core;

public class ConsumerArguments
{
    [Option('i', "input", Required = false, HelpText = "Set input queue", Default = "Consumer1")]
    public string InputQueue { get; set; }

}
