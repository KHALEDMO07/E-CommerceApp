namespace E_Commerce.Interfaces
{
    public interface IGetFileFromByteArray
    {
        IFormFile ConvertByteArrayToFile(byte[] byteArray, string fileName);
    }
}
