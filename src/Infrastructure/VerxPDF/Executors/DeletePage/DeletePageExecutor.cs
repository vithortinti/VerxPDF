using Flexcon.Anotations;
using Flexcon.Dependences;
using VerxPDF.Core.Helpers;
using VerxPDF.Core.Services;

namespace VerxPDF.Executors.DeletePage
{
#nullable disable
    [Option("delete")]
    public class DeletePageExecutor : Executor
    {
        private string _pdfFile;
        private string _saveDirectory;
        private int[] _pages;

        public override void Execute(string[] args)
        {
            DeletePdfPagesService deletePdfPagesService = new DeletePdfPagesService(_pdfFile);

            if (_pages.Length == 1)
                deletePdfPagesService.DeletePage(_pages[0], _saveDirectory);
            else
                deletePdfPagesService.DeletePages(_pages[0], _pages[1], _saveDirectory);
        }

        /// <summary>
        /// Pdf file parameter
        /// </summary>
        /// <param name="pdf"></param>
        /// <exception cref="ArgumentNullException"></exception>
        [Parameter("-p", Required = true)]
        public void Pdf(string pdf)
        {
            if (string.IsNullOrEmpty(pdf) || pdf.ElementAt(0) == '-' /*Possible parameter*/)
                throw new ArgumentNullException("Possible parameter identified.");

            _pdfFile = pdf;
        }

        /// <summary>
        /// Save directory parameter
        /// </summary>
        /// <param name="directory"></param>
        [Parameter("-d", Required = true)]
        public void Directory(string directory)
        {
            _saveDirectory = directory;
        }

        /// <summary>
        /// Page to remove parameter
        /// </summary>
        /// <param name="pages"></param>
        /// <exception cref="InvalidOperationException"></exception>
        [Parameter("-r", Required = true)]
        public void Remove(string pages)
        {
            if (string.IsNullOrEmpty(pages))
                throw new ArgumentNullException("At least one page must be specified, or a range.");

            try
            {
                // Just one page
                int page = int.Parse(pages);
                _pages = new int[] { page };
            }
            catch
            {
                // More than one page
                var intervalToDelete = pages.Split("to");

                if (intervalToDelete.Length > 2)
                    throw new InvalidOperationException("An interval must be specified between the first page to be deleted and the last page to be deleted.");
                else if (intervalToDelete.Length == 0 || intervalToDelete == null)
                    throw new InvalidOperationException("Incorrect way to specify a page range.");

                if (intervalToDelete[1] == "End")
                {
                    intervalToDelete[1] = PdfHelper.PageCount(_pdfFile).ToString();
                }
                // The total number of pages minus the specified number
                else if (intervalToDelete[1].Contains("End-"))
                {
                    int totalPages = PdfHelper.PageCount(_pdfFile);
                    var values = intervalToDelete[1].Replace("End", totalPages.ToString())
                        .Split('-');
                    intervalToDelete[1] = Convert.ToString(int.Parse(values[0]) - int.Parse(values[1]));
                }

                _pages = new int[2];
                for (int i = 0; i < intervalToDelete.Length; i++)
                {
                    _pages[i] = int.Parse(intervalToDelete[i]);
                }
            }
        }

        public override void Help()
        {
            Console.WriteLine("Delete:\n" +
                              "\t delete: Creates a new PDF without the pages specified for deletion.\n" +
                              "\t -p: Pdf file.\n" +
                              "\t -d: Save directory.\n" +
                              "\t -r: Pages to remove.\n" +
                              "\t\t {x}: Remove the x page.\n" +
                              "\t\t {x}to{y}: Removes from the page x to the page y specified.\n" +
                              "\t\t {x}toEnd: Removes from the page x to the last page of the document.\n" +
                              "\t\t {x}toEnd-{y}: Removes from the page x to the last page of the document minus y.");
            Console.WriteLine("\t -- HOW TO USE: verxpdf delete [-p <PDF-FILE>] [-d <DESTINATION-DIRECTORY>] [-r <PAGE-NUMBER | PAGE-INTERVAL>]");
            Console.WriteLine("\t -- EXAMPLE: verxpdf delete -p C:\\File.pdf -d C:\\Folder -r [3 | 3to9 | 3toEnd | 3toEnd-2]");
            Console.WriteLine("\t The parameters do not have a defined order of use, except for the main parameter \"delete\".");
        }
    }
}
