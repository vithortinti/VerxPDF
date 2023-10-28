/**
 * This class uses the PdfSharp library, which uses the MIT license.
 * 
 * You can find out more about this license here: https://mit-license.org/
 * **/

using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace VerxPDF.Core.Services
{
    public class DeletePdfPagesService
    {
        private PdfDocument _pdfDocument;
        private string _newPdfFileName;

        public DeletePdfPagesService(string pdf)
        {
            _pdfDocument = PdfReader.Open(pdf, PdfDocumentOpenMode.Import);
            _newPdfFileName = Path.GetFileNameWithoutExtension(pdf) + "-DltPages.pdf";
        }

        /// <summary>
        /// Delete a single page from PDF file.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="saveDirectory"></param>
        public void DeletePage(int page, string saveDirectory = null!)
        {
            page -= 1;
            using (PdfDocument newPdf = new PdfDocument())
            {
                Console.WriteLine("Creating new PDF File.");
                for (int i = 0; i < _pdfDocument.Pages.Count; i++)
                {
                    if (i == page)
                    {
                        Console.WriteLine($"Delete page {i + 1}");
                        continue;
                    }

                    newPdf.AddPage(_pdfDocument.Pages[i]);
                }

                if (string.IsNullOrEmpty(saveDirectory))
                    newPdf.Save(Directory.GetCurrentDirectory() + "\\" + _newPdfFileName);
                else
                    newPdf.Save(saveDirectory + "\\" + _newPdfFileName);
            }
        }

        /// <summary>
        /// Delete a range of pages from PDF file.
        /// </summary>
        /// <param name="firstPage"></param>
        /// <param name="lastPage"></param>
        /// <param name="saveDirectory"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void DeletePages(int firstPage, int lastPage, string saveDirectory = null!)
        {
            if (firstPage == 1 && lastPage == _pdfDocument.Pages.Count)
                throw new InvalidOperationException("It is not possible to delete all PDF pages.");

            firstPage -= 1;
            lastPage -= 1;

            using (PdfDocument newPdf = new PdfDocument())
            {
                Console.WriteLine("Creating new PDF File.");
                for (int i = 0; i < _pdfDocument.Pages.Count; i++)
                {
                    if (i >= firstPage && i <= lastPage)
                    {
                        Console.WriteLine($"Delete page {i + 1}");
                        continue;
                    }

                    newPdf.AddPage(_pdfDocument.Pages[i]);
                }

                if (string.IsNullOrEmpty(saveDirectory))
                    newPdf.Save(Directory.GetCurrentDirectory() + "\\" + _newPdfFileName);
                else
                    newPdf.Save(saveDirectory + "\\" + _newPdfFileName);
            }
        }

        /// <summary>
        /// Delete pages from PDF file.
        /// </summary>
        /// <param name="pages"></param>
        /// <param name="saveDirectory"></param>
        public void DeletePages(int[] pages, string saveDirectory = null!)
        {
            pages = pages.Select(x => x - 1).ToArray();

            using (PdfDocument newPdf = new PdfDocument())
            {
                Console.WriteLine("Creating new PDF File.");
                int arrIndex = 0;
                for (int i = 0; i < _pdfDocument.Pages.Count; i++)
                {
                    if (pages[arrIndex] == i)
                    {
                        Console.WriteLine($"Delete page {i + 1}");
                        arrIndex++;
                        if (arrIndex >= pages.Length) arrIndex--; // Prevents out of bounds exception
                        continue;
                    }

                    newPdf.AddPage(_pdfDocument.Pages[i]);
                }

                if (string.IsNullOrEmpty(saveDirectory))
                    newPdf.Save(Directory.GetCurrentDirectory() + "\\" + _newPdfFileName);
                else
                    newPdf.Save(saveDirectory + "\\" + _newPdfFileName);
            }
        }
    }
}
