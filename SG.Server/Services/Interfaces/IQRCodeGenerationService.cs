using Avalonia.Media.Imaging;

namespace SG.Server.Services.Interfaces
{
    public interface IQRCodeGenerationService
    {
        byte[] GenerateQRCodeImage(string jsonInput);
        Bitmap? GenerateQRCodeAndConvertToBitmapImage(string jsonInput);
    }
}
