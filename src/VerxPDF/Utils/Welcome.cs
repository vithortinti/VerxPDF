namespace VerxPDF.Utils
{
    public static class Welcome
    {
        public static void ShowWelcomeMessage()
        {
            Console.WriteLine(new string('=', 24));
            Console.WriteLine("|| WELCOME TO VERXPDF ||");
            Console.WriteLine(new string('=', 24));
            Console.WriteLine();
            Console.WriteLine("VerxPDF is free, open source software designed to convert PDF files to images, delete pages and merge PDF files.");
            Console.WriteLine("At the moment, the software is limited to these three functions, but there are opportunities for growth.");
            Console.WriteLine("For help on how to use it, type --help.");
            Console.WriteLine();
            Console.WriteLine("Github: https://github.com/vithortinti/VerxPDF");
        }
    }
}
