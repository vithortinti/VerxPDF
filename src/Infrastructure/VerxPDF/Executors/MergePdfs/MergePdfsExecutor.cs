using Flexcon.Anotations;
using Flexcon.Dependences;
using VerxPDF.Core.Services;

namespace VerxPDF.Executors.MergePdfs
{
#nullable disable
    [Option("merge")]
    public class MergePdfsExecutor : Executor
    {
        private string[] _pdfs;
        private string _directory;
        private string _pdfName;

        public override void Execute(string[] args)
        {
            MergePdfService service = new MergePdfService(_pdfs);

            try
            {
                service.Merge(_directory, _pdfName);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Pdfs parameter
        /// </summary>
        /// <param name="pdfs"></param>
        /// <exception cref="InvalidOperationException"></exception>
        [Parameter("-p", Required = true)]
        public void Pdfs(string[] pdfs)
        {
            if (pdfs.Length < 2)
                throw new InvalidOperationException("There must be two or more PDFs specified for execution.");

            _pdfs = pdfs;
        }

        /// <summary>
        /// Directory destination parameter
        /// </summary>
        /// <param name="directory"></param>
        [Parameter("-d", Required = true)]
        public void Directory(string directory)
        {
            _directory = directory;
        }

        /// <summary>
        /// Pdf name parameter
        /// </summary>
        /// <param name="name"></param>
        [Parameter("-n")]
        public void PdfName(string name)
        {
            _pdfName = name;
        }

        public override void Help()
        {
            Console.WriteLine("Merge:");
            Console.WriteLine("\t merge: Merge all PDFs into 1.");
            Console.WriteLine("\t -p: Pdf files. You must specify 2 or more PDF paths.");
            Console.WriteLine("\t -d: New Pdf file save directory.");
            Console.WriteLine("\t -n: New Pdf file name (optional).");
            Console.WriteLine("\t -- HOW TO USE: verxpdf merge [-p <PDF-FILES>] [-d <DESTINATION-DIRECTORY>] [-n <NEW-PDF-NAME> OPTIONAL]");
            Console.WriteLine("\t -- EXAMPLE: verxpdf merge -p C:\\File.pdf C:\\File2.pdf -d C:\\Folder -n MyMergedPdf");
            Console.WriteLine("\t The parameters do not have a defined order of use, except for the main parameter \"merge\".");
        }
    }
}
