/**
 * This class uses the PdfiumViewer library, which uses the APACHE 2.0 license.
 * 
 * You can find out more about this license here: http://www.apache.org/licenses/LICENSE-2.0
 * **/

using PdfiumViewer;
using System.Drawing;
using VerxPDF.Core.Utils;

namespace VerxPDF.Core.Services
{
    public class PdfToJpgService
    {
        private PdfDocument _pdfDocument;
        private string _path;

        public PdfToJpgService(string path)
        {
            _path = path;
            _pdfDocument = PdfDocument.Load(path);
        }

        public void ToImage(string savePath)
        {
            ConvertToImage(savePath);
        }

        public void ToImage(string savePath, int width, int height)
        {
            ConvertToImage(savePath, width, height);
        }

        private void ConvertToImage(string savePath, int width = 0, int height = 0)
        {
            int pgCount = _pdfDocument.PageCount;

            if (width > 0 && height > 0)
            {
                for (int i = 0; i < pgCount; i++)
                {
                    using (Bitmap imagem = (Bitmap)_pdfDocument.Render(i, width, height, 300, 300, true))
                    {
                        imagem.Save(GetImagePath(savePath) + $"_{i}.jpg");
                    }

                    ShowLog(() => ProgressBar.Update(i + 1, pgCount));
                }
            }
            else
            {
                for (int i = 0; i < pgCount; i++)
                {
                    using (Bitmap imagem = (Bitmap)_pdfDocument.Render(i, 300, 300, true))
                    {
                        imagem.Save(GetImagePath(savePath) + $"_{i}.jpg");
                    }

                    ShowLog(() => ProgressBar.Update(i + 1, pgCount));
                }
            }

            ShowLog("\nProcess completed.");
        }

        private string GetImagePath(string path)
        {
            string fileName = Path.GetFileNameWithoutExtension(_path);

            return Path.Combine(path, fileName);
        }

        private void ShowLog(string message)
        {
            Console.WriteLine(message);
        }

        private void ShowLog(Action action)
        {
            action.Invoke();
        }
    }
}
