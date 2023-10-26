using Flexcon.Anotations;
using Flexcon.Dependences;
using VerxPDF.Application.Configurations;
using VerxPDF.Domain.Interfaces.Configuration.ImageSize;
using VerxPDF.Domain.Models;
using VerxPDF.Domain.Models.Files;
using VerxPDF.Persistence.Repository;

namespace VerxPDF.Executors.ConvertToJpg.Configuration
{
    [Option("image-config")]
    public class ConvertToJpgConfigurationExecutor : Executor
    {
        private readonly IImageSizesConfiguration _imageSizesConfiguration;

        public ConvertToJpgConfigurationExecutor()
        {
            // TODO: Aplicar injeção de dependência nas próximas release do Flexcon.
            _imageSizesConfiguration = new ImageSizesConfiguration(new ImageSizeJsonRepository());
        }

        public override void Execute(string[] args)
        {
            if (args.Length == 0)
            {
                throw new ArgumentException("No argument provided.");
            }
        }

        /// <summary>
        /// Create a custom size for when you convert the PDF to images.
        /// </summary>
        /// <param name="name">Size name</param>
        /// <param name="size">Image size - [Width]x[Height]</param>
        [Parameter("--create-size")]
        public void CreateConfig(string name, string size)
        {
            try
            {
                _imageSizesConfiguration.Create(name, size);

                Console.WriteLine($"{name} size configuration created successfully.\n" +
                    $"You can type 'verxpdf image-config --show-sizes' to check all the available sizes.");
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong when creating the configuration. " +
                    "Check if the size was entered correctly or if the size doesn't exist.\n" +
                    $"For more information: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a configured size.
        /// </summary>
        /// <param name="name"></param>
        [Parameter("--delete-size")]
        public void DeleteConfig(string name)
        {
            try
            {
                _imageSizesConfiguration.Delete(name);

                Console.WriteLine("Configuration deleted successfully.");
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong when you tried to delete the configuration. " +
                    "Check that the name was entered correctly or that it exists.\n" +
                    $"For more information: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates a configured size.
        /// </summary>
        /// <param name="name"></param>
        [Parameter("--update-size")]
        public void UpdateConfig(string name, string size)
        {
            try
            {
                _imageSizesConfiguration.Update(name, size);

                Console.WriteLine("Configuration updated successfully.");
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong when you tried to update the configuration. " +
                    "Check that the name was entered correctly or that it exists.\n" +
                    $"For more information: {ex.Message}");
            }
        }

        /// <summary>
        /// Shows all configured sizes.
        /// </summary>
        [Parameter("--show-sizes")]
        public void ShowAllSizes()
        {
            try
            {
                var sizes = _imageSizesConfiguration.GetAll();

                if (sizes is not null)
                {
                    foreach (var size in sizes)
                    {
                        Console.WriteLine($"{size.Name}: {size.Size.Width}x{size.Size.Height}");
                    }
                }
                else
                {
                    Console.WriteLine("No size configured.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong when you tried to get the configuration. " +
                    "Check that the name was entered correctly or that it exists.\n" +
                    $"For more information: {ex.Message}");
            }
        }

        /// <summary>
        /// Shows a configured size.
        /// </summary>
        /// <param name="sizeName"></param>
        /// <exception cref="Exception"></exception>
        [Parameter("--show-size")]
        public void ShowSize(string sizeName)
        {
            try
            {
                var size = _imageSizesConfiguration.Get(sizeName);

                Console.WriteLine($"{size!.Name}: {size.Size.Width}x{size.Size.Height}");
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong when you tried to get the configuration. " +
                    "Check that the name was entered correctly or that it exists.\n" +
                    $"For more information: {ex.Message}");
            }
        }

        public override void Help()
        {
            Console.WriteLine("Image Configuration:");
            Console.WriteLine("image-config: Configuration when converting PDF to images.");
            Console.WriteLine("--create-size: Stores a custom size for the image it will be converted into with the name you choose.");
            Console.WriteLine("--delete-size: Deletes a configured size.");
            Console.WriteLine("--update-size: Updates a configured size.");
            Console.WriteLine("--show-sizes: Shows all configured sizes.");
            Console.WriteLine("--show-size: Shows a configured size.");
            Console.WriteLine("-- HOW TO USE: verxpdf image-config [--create-size <SIZE-NAME> <SIZE> | --update-size <SIZE-NAME> <NEW-SIZE> | --delete-size <SIZE-NAME> | --show-size <SIZE-NAME> | --show-sizes]");
            Console.WriteLine("The parameters do not have a defined order of use, except for the main parameter \"image-config\".");
        }
    }
}
