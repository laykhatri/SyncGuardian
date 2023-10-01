namespace SyncGuardianWpf.Services.Interfaces
{
    public interface IQRCodeGenerationService
    {
        byte[] GenerateQRCodeImage(string jsonInput);
    }
}
