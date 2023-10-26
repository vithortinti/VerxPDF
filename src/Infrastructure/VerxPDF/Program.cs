using Flexcon;
using System.Reflection;
using VerxPDF.Utils;

if (args.Length == 0)
{
    Welcome.ShowWelcomeMessage();
}
else
{
    try
    {
        Application app = new Application(Assembly.GetExecutingAssembly(), args);
        app.Configure(
            parameterIdentifier: '-'
            );
        app.Run();
    }
    catch (Exception ex)
    {
        string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
        Message.WriteLineError($"An unexpected error has occurred during execution. \nDetails: {message}\nType \"verxpdf --help\" for help.");
    }
}