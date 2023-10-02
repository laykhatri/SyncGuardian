using System.Windows.Media.Imaging;

namespace SyncGuardianWpf.Services.Interfaces
{
    public interface IQRCodeGenerationService
    {
        byte[] GenerateQRCodeImage(string jsonInput);
        BitmapImage GenerateQRCodeAndConvertToBitmapImage(string jsonInput);
    }
}
