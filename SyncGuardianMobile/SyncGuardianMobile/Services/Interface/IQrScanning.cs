using System.Threading.Tasks;

namespace SyncGuardianMobile.Services.Interface
{
    public interface IQrScanning
    {
        Task<string> ScanAsync();
    }
}
