using VerxPDF.Domain.Models.Files;

namespace VerxPDF.Domain.Interfaces.Configuration.ImageSize
{
    public interface IImageSizesConfiguration
    {
        ImageConfiguration? Get(string name);
        List<ImageConfiguration>? GetAll();
        void Update(string name, string newSize);
        void Create(string name, string size);
        void Delete(string sizeName);
    }
}
