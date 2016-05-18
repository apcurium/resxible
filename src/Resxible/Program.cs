using System;
using Mono.Options;

namespace Com.Apcurium.Resxible
{
    class Program
    {
        static void Main(string[] args)
        {
            var target = string.Empty;
            var source = string.Empty;
            var commonSource = string.Empty;
            var destination = string.Empty;
            var backup = false;
            var clearDestination = false;

            var p = new OptionSet
            {
                {"t|target=", "Target application: ios or android", t => target = t.ToLowerInvariant()},
                {"c|common=", "Common .resx file path", c => commonSource = c},
                {"m|master=", "Master .resx file path", m => source = m},
                {"d|destination=", "Destination file path", d => destination = d},
                {"b|backup", "Backup file", b => backup = b != null},
                {"clear", "Clear destination files", c => clearDestination = c != null}
            };

            try
            {
                p.Parse(args);
            }
            catch (OptionException e)
            {
                ShowHelpAndExit(e.Message, p);
            }

            try
            {
                ResourcesConverter.Generate(source, commonSource, destination, target, backup, clearDestination);

                Console.WriteLine("Localization tool ran successfully.");
            }
            catch (Exception exception)
            {
                Console.Write("error: ");
                Console.WriteLine(exception.ToString());
            }
        }

        private static void ShowHelpAndExit(string message, OptionSet optionSet)
        {
            Console.Error.WriteLine(message);
            optionSet.WriteOptionDescriptions(Console.Error);
            Environment.Exit(-1);
        }
    }
}
