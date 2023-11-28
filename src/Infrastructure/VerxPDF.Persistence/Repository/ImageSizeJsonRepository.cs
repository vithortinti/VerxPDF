using Newtonsoft.Json;
using VerxPDF.Domain.Models.Files;
using VerxPDF.Domain.Interfaces.Repositories.ImageSizesFileRepository;
using System.Reflection;

namespace VerxPDF.Persistence.Repository
{
    public class ImageSizeJsonRepository : IImageSizesFileRepository
    {
        private readonly string? _file;
        private readonly List<ImageConfiguration> _imageConfiguration;

        public ImageSizeJsonRepository()
        {
            string directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
            string file = directory + "\\Configs\\image-sizes.json";
            _file = file;
            if (!File.Exists(file))
                CreateNewConfiguration(file);

            string fileContent = File.ReadAllText(file);
 
            if (!string.IsNullOrEmpty(fileContent))
            {
                _imageConfiguration = JsonConvert.DeserializeObject<List<ImageConfiguration>>(fileContent)!;
            }
            else
            {
                _imageConfiguration = new List<ImageConfiguration>();
            }
        }

        /// <summary>
        /// Get the configuration available in the configuration file.
        /// </summary>
        /// <returns>File configuration model</returns>
        public ImageConfiguration? Get(string name)
        {
            try
            {
                return _imageConfiguration.FirstOrDefault(x => x.Name == name);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get all the settings available in the file.
        /// </summary>
        /// <returns></returns>
        public List<ImageConfiguration>? GetAll()
        {
            return _imageConfiguration;
        }

        /// <summary>
        /// Add a new configuration.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public void Create(ImageConfiguration model)
        {
            // Checking if the size already exists
            var imageSize = _imageConfiguration.FirstOrDefault(x => x.Name == model.Name);
            if (imageSize is not null) throw new Exception($"Identifier with the same value found, ending the operation.");

            _imageConfiguration.Add(model);
        }

        /// <summary>
        /// Saves the configuration.
        /// </summary>
        public void Save()
        {
            string newConfiguration = JsonConvert.SerializeObject(_imageConfiguration, Formatting.Indented);
            File.WriteAllText(_file!, newConfiguration);
        }

        /// <summary>
        /// Changes the Width and Height of a configuration.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(ImageConfiguration model)
        {
            int configIndex = _imageConfiguration.FindIndex(x => x.Name == model.Name);
            if (configIndex < 0) return false;

            _imageConfiguration[configIndex] = model;
            return true;
        }

        /// <summary>
        /// Removes a configuration from the file.
        /// </summary>
        /// <param name="model"></param>
        public bool Delete(ImageConfiguration model)
        {
            return _imageConfiguration.Remove(model);
        }

        /// <summary>
        /// Creates a new configuration file for the images option and prepares the json.
        /// </summary>
        /// <param name="file"></param>
        private static void CreateNewConfiguration(string file)
        {
            if (!File.Exists(file))
            {
                string directory = Path.GetDirectoryName(file)!;
                if (!Path.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                File.Create(file).Close();
            }
        }
    }
}
