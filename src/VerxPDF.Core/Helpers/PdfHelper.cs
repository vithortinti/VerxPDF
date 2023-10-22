using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace VerxPDF.Core.Helpers
{
    public static class PdfHelper
    {
        public static int PageCount(string pdf)
        {
            using (PdfDocument pdfDocument = PdfReader.Open(pdf))
            {
                return pdfDocument.Pages.Count;
            }
        }
    }
}
