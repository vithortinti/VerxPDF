using Flexcon.Anotations;
using Flexcon.Dependences;
using System.Reflection;

namespace VerxPDF.Executors.Help
{
#nullable disable
    [Option("--help")]
    public class HelpExecutor : Executor
    {
        public override void Execute(string[] args)
        {
            string executor = string.Empty;
            if (args.Length == 1)
                executor = args[0].ToLower();

            var assembly = Assembly.GetExecutingAssembly();
            var executorClasses = assembly.GetTypes()
                .Where(x => x.BaseType == typeof(Executor));

            Console.WriteLine(new string('=', 20) + "VerxPDF" + new string('=', 20));
            Console.WriteLine();

            // Executes the help for the specified class
            if (!string.IsNullOrEmpty(executor))
            {
                var exe = executorClasses.FirstOrDefault(x => ((Option)x.GetCustomAttribute(typeof(Option))).Value == executor);
                if (exe == null)
                    throw new Exception($"The option {executor} does not exist.");
                var executorClass = Activator.CreateInstance(exe);
                MethodInfo helpMethod = exe.GetMethod("Help");
                helpMethod.Invoke(executorClass, null);
            }
            else
            {
                // Executes all helps
                foreach (var exe in executorClasses)
                {
                    var executorClass = Activator.CreateInstance(exe);
                    MethodInfo helpMethod = exe.GetMethod("Help");
                    helpMethod!.Invoke(executorClass, null);
                    Console.WriteLine();
                }
            }
        }

        public override void Help()
        {
            Console.WriteLine("Help:");
            Console.WriteLine("\t --help: Help parameter, which shows all available options and parameters.");
            Console.WriteLine("\t --help {option} (optional): Shows the help for a specific option.");
            Console.WriteLine("\t -- HOW TO USE: verxpdf --help [<OPTION> OPTIONAL]");
            Console.WriteLine("\t -- EXAMPLE: verxpdf --help image");
            Console.WriteLine("\t The parameters have a defined order of use.");
        }
    }
}
