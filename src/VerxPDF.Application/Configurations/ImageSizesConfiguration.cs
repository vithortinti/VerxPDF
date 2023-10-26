using System.Data.Common;
using VerxPDF.Domain.Interfaces.Configuration.ImageSize;
using VerxPDF.Domain.Interfaces.Repositories.ImageSizesFileRepository;
using VerxPDF.Domain.Models;
using VerxPDF.Domain.Models.Files;

namespace VerxPDF.Application.Configurations
{
    public class ImageSizesConfiguration : IImageSizesConfiguration
    {
        private readonly IImageSizesFileRepository _imageSizeFileRepository;

        public ImageSizesConfiguration(IImageSizesFileRepository imageSizesFileRepository)
        {
            _imageSizeFileRepository = imageSizesFileRepository;
        }

        public List<ImageConfiguration>? GetAll()
        {
            return _imageSizeFileRepository.GetAll() 
                ?? throw new NullReferenceException("There are no configurations.");
        }

        public ImageConfiguration? Get(string name)
        {
            return _imageSizeFileRepository.Get(name) 
                ?? throw new NullReferenceException($"There is no configuration with the name {name}.");
        }

        public void Create(string name, string size)
        {
            // Checking if the size already exists
            var config = _imageSizeFileRepository.Get(name);
            if (config != null)
                throw new Exception($"The {name} size already exists.");

            // Checking if the size is valid
            int[] wh = size.Split('x')
                .Select(x => Convert.ToInt32(x))
                .ToArray();

            // Creating a new page size
            ImageConfiguration newConfig = new()
            {
                Name = name,
                Size = new PageSize(wh[0], wh[1])
            };

            // Creating the new size
            _imageSizeFileRepository.Create(newConfig);
            _imageSizeFileRepository.Save();
        }

        public void Update(string name, string newSize)
        {
            // Checking if the size exists
            var imageSize = _imageSizeFileRepository.Get(name) 
                ?? throw new NullReferenceException($"There is no configuration with the name {name}.");

            // Checking if the new size is valid
            int[] wh = newSize.Split('x')
                .Select(x => Convert.ToInt32(x))
                .ToArray();

            // Creating a new page size
            ImageConfiguration newConfig = new()
            {
                Name = name,
                Size = new(wh[0], wh[1])
            };

            // Updating the size
            var updated = _imageSizeFileRepository.Update(newConfig);
            if (!updated)
                throw new Exception("It was not possible to update the configuration.");
            _imageSizeFileRepository.Save();
        }

        public void Delete(string sizeName)
        {
            // Checking if the size exists
            var config = _imageSizeFileRepository.Get(sizeName) 
                ?? throw new NullReferenceException($"The {sizeName} size does not exist for deletion.");

            // Deleting the size
            bool deleted = _imageSizeFileRepository.Delete(config!);
            if (!deleted)
                throw new Exception("It was not possible to delete the configuration.");
            _imageSizeFileRepository.Save();
        }
    }
}