using Flexcon.Anotations;
using Flexcon.Dependences;
using VerxPDF.Core.Services;
using VerxPDF.Domain.Models;

namespace VerxPDF.Executors.ConvertToJpg
{
#nullable disable
    [Option("image")]
    public class ConvertToJpgExecutor : Executor
    {
        private string _pdfFile;
        private string _destination;
        private PageSize _pageSize;
        private PdfToJpgService _service;

        public override void Execute(string[] args)
        {
            _service = new PdfToJpgService(_pdfFile);

            try
            {
                if (_pageSize != null)
                {
                    _service.ToImage(_destination, _pageSize.Width, _pageSize.Height); // Specified Size
                }
                else
                {
                    _service.ToImage(_destination); // Default size
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Pdf File parameter
        /// </summary>
        /// <param name="filename"></param>
        [Parameter("-p", Required = true)]
        public void Pdf(string filename)
        {
            _pdfFile = filename;
        }

        /// <summary>
        /// Destination Parameter
        /// </summary>
        /// <param name="destination"></param>
        [Parameter("-d", Required = true)]
        public void Destination(string destination)
        {
            _destination = destination;
        }

        /// <summary>
        /// Size parameter
        /// </summary>
        /// <param name="size"></param>
        [Parameter("-s")]
        public void Size(string size)
        {
            if (size.Contains("x", StringComparison.InvariantCultureIgnoreCase))
            {
                // Width x Height
                int[] wh = size.Split("x")
                    .Select(x => Convert.ToInt32(x))
                    .ToArray();

                bool isValid = wh.All(x => x > 0);
                if (isValid)
                {
                    int xIndex = 0;
                    int yIndex = 1;
                    _pageSize = new PageSize(wh[xIndex], wh[yIndex]);
                }
                else
                {
                    throw new Exception("The offered size parameters must be greater than 0 with integer values.");
                }
            }
            else if (size == "a4")
            {
                int width = 2480;
                int height = 3508;

                _pageSize = new PageSize(width, height);
            }
            else if (size == "full-hd")
            {
                int width = 1920;
                int height = 1080;

                _pageSize = new PageSize(width, height);
            }
            else if (size == "slide")
            {
                int width = 960;
                int height = 720;

                _pageSize = new PageSize(width, height);
            }
            else
            {
                throw new Exception($"Invalid size parameter '{size}'.");
            }
        }

        /// <summary>
        /// Quality parameter
        /// </summary>
        /// <param name="quality"></param>
        [Parameter("-q", ReferenceTo = "-s")]
        public void Quality(string quality)
        {
            if (string.IsNullOrEmpty(quality)) throw new Exception("Quality parameter value not set.");

            switch (quality)
            {
                case "high":
                    _pageSize.Width *= 4;
                    _pageSize.Height *= 4;
                    break;
                case "normal":
                    // Nothing changes
                    break;
                case "low":
                    _pageSize.Width /= 4;
                    _pageSize.Height /= 4;
                    break;
                default:
                    throw new Exception($"Invalid image quality: {(quality == string.Empty ? "Unspecified quality" : quality)}.");
            }
        }

        public override void Help()
        {
            Console.WriteLine("Image:");
            Console.WriteLine("\t image: Convert PDF to JPG files.");
            Console.WriteLine("\t -p: Pdf file.");
            Console.WriteLine("\t -d: Save directory.");
            Console.WriteLine("\t -s: Image size parameter (optional).");
            Console.WriteLine("\t\t slide: Creates a JPG image with size 960x720;");
            Console.WriteLine("\t\t full-hd: Creates a JPG image with size 1920x1080;");
            Console.WriteLine("\t\t a4: Creates a JPG image with size 2480x3508;");
            Console.WriteLine("\t\t {Width}x{Height}: Creates a JPG image with the specified size in pixels. Example: 1000x1000.");
            Console.WriteLine("\t -q: Image quality parameter (optional). Must to be used with size parameter.");
            Console.WriteLine("\t\t low: Low image quality (faster) - Divide the number of pixels by 4;");
            Console.WriteLine("\t\t normal: Normal image quality (default option) - Uses the size specified in the size parameter;");
            Console.WriteLine("\t\t high: High image quality (slower) - Multiplies the number of pixels by 4.");
            Console.WriteLine("\t -- HOW TO USE: verxpdf image [-p <PDF-FILE>] [-d <DESTINATION-DIRECTORY>] [-s <SIZE> OPTIONAL] [-q <QUALITY> OPTIONAL]");
            Console.WriteLine("\t -- EXAMPLE: verxpdf image -p C:\\File.pdf -d C:\\Folder -s a4 -q normal");
            Console.WriteLine("\t The parameters do not have a defined order of use, except for the main parameter \"image\".");
        }
    }
}