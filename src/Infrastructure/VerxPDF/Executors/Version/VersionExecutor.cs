using Flexcon.Anotations;
using Flexcon.Dependences;
using System.Reflection;

namespace VerxPDF.Executors.Version
{
    [Option("--version")]
    public class VersionExecutor : Executor
    {
        public override void Execute(string[] args)
        {
            string version = Assembly.GetExecutingAssembly()
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()!
                .InformationalVersion;

            Console.WriteLine("VerxPDF " + version + " - A free and open source PDF converter.");
        }

        public override void Help()
        {
            Console.WriteLine("Version:");
            Console.WriteLine("\t --version: App version");
        }
    }
}