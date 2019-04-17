using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GConfig
{
    class Program
    {
        public class Options
        {
            [Option('s', "setting", Required = true, HelpText = "Setting file.")]
            public string Setting { get; set; }
        }

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed<Options>(o =>
                   {
                       if (!string.IsNullOrEmpty(o.Setting))
                       {
                           var settingPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, o.Setting);
                           Console.WriteLine($"input path {settingPath}");
                       }
                   });

            
        }
    }
}
