using E_Commerce.Interfaces;

namespace E_Commerce.Services
{
    public class GetFileFromByteArray :IGetFileFromByteArray
    {
       public IFormFile ConvertByteArrayToFile(byte[] byteArray, string fileName)
        {
            var stream = new MemoryStream(byteArray);
            var file = new FormFile(stream, 0, byteArray.Length, "file", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/octet-stream"
            };

            return file;
        }
    }
}
