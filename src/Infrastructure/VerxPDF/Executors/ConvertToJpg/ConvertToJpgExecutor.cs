using Flexcon.Anotations;
using Flexcon.Dependences;
using VerxPDF.Core.Services;
using VerxPDF.Domain.Models;
using VerxPDF.Application.Configurations;
using VerxPDF.Domain.Interfaces.Configuration.ImageSize;
using VerxPDF.Persistence.Repository;

namespace VerxPDF.Executors.ConvertToJpg
{
#nullable disable
    [Option("image")]
    public class ConvertToJpgExecutor : Executor
    {
        private string _pdfFile;
        private string _destination;
        private PageSize _pageSize;
        private IImageSizesConfiguration _imageSizesConfiguration;
        private PdfToJpgService _service;

        public ConvertToJpgExecutor()
        {
            _imageSizesConfiguration = new ImageSizesConfiguration(new ImageSizeJsonRepository());
        }

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
            var imageSize = _imageSizesConfiguration.Get(size);

            if (size.Contains('x', StringComparison.InvariantCultureIgnoreCase))
            {
                // Width x Height
                int[] wh = size.Split('x')
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
            else if (imageSize is not null)
            {
                int width = imageSize.Size.Width;
                int height = imageSize.Size.Height;

                _pageSize = new PageSize(width, height);
            }
            else
            {
                throw new Exception($"There is no size {size}, but you can create a custom size.");
            }
        }

        /// <summary>
        /// Quality parameter
        /// </summary>
        /// <param name="quality"></param>
        [Parameter("-q", ReferenceTo = "-s")]
        public void Quality(int quality)
        {
            if (quality <= 0) throw new ArgumentNullException("Quality must be greater than 0.");

            // Multiplies the pixels
            _pageSize.Width *= quality;
            _pageSize.Height *= quality;
        }

        public override void Help()
        {
            Console.WriteLine("PDF TO JPG:");
            Console.WriteLine("image: Convert PDF to JPG files.");
            Console.WriteLine("-p: Pdf file.");
            Console.WriteLine("-d: Save directory.");
            Console.WriteLine("-s: Image size parameter (optional). You can see more size options with \"verxpdf --help image-config\".");
            Console.WriteLine("{Width}x{Height}: Creates a JPG image with the specified size in pixels. Example: 1000x1000.");
            Console.WriteLine("-q: Image quality parameter (optional). Multiplies the image size by the integer entered.");
            Console.WriteLine("HOW TO USE: verxpdf image [-p <PDF-FILE>] [-d <DESTINATION-DIRECTORY>] [-s <SIZE> OPTIONAL] [-q <QUALITY> OPTIONAL]");
            Console.WriteLine("The parameters do not have a defined order of use, except for the main parameter \"image\".");
        }
    }
}