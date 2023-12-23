using SyncGuardianMobile.Services.Interface;
using System.Threading.Tasks;
using ZXing.Mobile;

namespace SyncGuardianMobile.Services
{
    public class QrScanning : IQrScanning
    {
        public async Task<string> ScanAsync()
        {
            var scanningOptions = new MobileBarcodeScanningOptions() { TryHarder = true, AutoRotate = true };
            var scanner = new MobileBarcodeScanner()
            {
                TopText = "Scan the SyncGuardian QR Code",
                BottomText = "Please Wait",
            };

            var scanResult = await scanner.Scan(scanningOptions);

            return scanResult != null ? scanResult.Text : string.Empty;
        }
    }
}
