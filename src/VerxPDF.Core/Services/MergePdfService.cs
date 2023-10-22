/**
 * This class uses the PdfSharp library, which uses the MIT license.
 * 
 * You can find out more about this license here: https://mit-license.org/
 * **/

using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using VerxPDF.Core.Utils;

namespace VerxPDF.Core.Services
{
    public class MergePdfService
    {
        private string[] _pdfs;

        public MergePdfService(string[] pdfs)
        {
            _pdfs = pdfs;
        }

        public void Merge(string savePath, string pdfName = null!)
        {
            using (PdfDocument newPdf = new PdfDocument())
            {
                foreach (string pdf in _pdfs)
                {
                    if (!File.Exists(pdf))
                        throw new FileNotFoundException(pdf);

                    using (PdfDocument pdfDoc = PdfReader.Open(pdf, PdfDocumentOpenMode.Import))
                    {
                        for (int i = 0; i < pdfDoc.PageCount; i++)
                        {
                            newPdf.AddPage(pdfDoc.Pages[i]);
                            ProgressBar.Update(i + 1, pdfDoc.PageCount, Path.GetFileName(pdf));
                        }
                        Console.WriteLine();
                    }
                }

                if (pdfName != null)
                {
                    newPdf.Save(savePath + "\\" + $"{pdfName}.pdf");
                }
                else
                {
                    string data = DateTime.Now.ToString("dd/MM/yyyy_HH:mm:ss").Replace("/", "").Replace(":", "");
                    newPdf.Save(savePath + "\\" + $"MergedPDF-{data}.pdf");
                }

                Console.WriteLine("\nProcess completed.");
            }
        }
    }
}
