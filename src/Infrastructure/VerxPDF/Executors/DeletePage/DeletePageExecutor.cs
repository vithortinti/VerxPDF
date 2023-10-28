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
        private bool _isRange;
        private int[] _pages;

        public override void Execute(string[] args)
        {
            DeletePdfPagesService deletePdfPagesService = new DeletePdfPagesService(_pdfFile);

            if (_isRange)
            {
                deletePdfPagesService.DeletePages(_pages[0], _pages[1], _saveDirectory);
            }
            else
            {
                deletePdfPagesService.DeletePages(_pages, _saveDirectory);
            }
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
        public void Remove(string[] pages)
        {
            if (pages == null || pages.Length == 0)
                throw new ArgumentNullException("At least one page must be specified, or a range.");

            try
            {
                // Specific pages
                _pages = pages.Select(int.Parse)
                    .ToArray();
            }
            catch
            {
                // A range of pages
                _isRange = true;
                var intervalToDelete = pages[0].Split("to");

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
            Console.WriteLine("DELETE PDF PAGES:\n" +
                              "delete: Creates a new PDF without the pages specified for deletion.\n" +
                              "-p: Pdf file.\n" +
                              "-d: Save directory.\n" +
                              "-r: Pages to remove.\n" +
                              "{x}: Remove the x page.\n" +
                              "{x}to{y}: Removes from the page x to the page y specified.\n" +
                              "{x}toEnd: Removes from the page x to the last page of the document.\n" +
                              "{x}toEnd-{y}: Removes from the page x to the last page of the document minus y.");
            Console.WriteLine("HOW TO USE: verxpdf delete [-p <PDF-FILE>] [-d <DESTINATION-DIRECTORY>] [-r <PAGE-NUMBER | PAGE-INTERVAL>]");
            Console.WriteLine("The parameters do not have a defined order of use, except for the main parameter \"delete\".");
        }
    }
}
