using QRCoder;
using Serilog;
using SyncGuardianWpf.Services.Interfaces;
using System;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Windows.Media.Imaging;

namespace SyncGuardianWpf.Services
{
    public class QRCodeGenerationService : IQRCodeGenerationService
    {
        private readonly ILogger _logger;

        public QRCodeGenerationService(ILogger logger)
        {
            _logger = logger;
        }

        public byte[] GenerateQRCodeImage(string jsonInput)
        {
            try
            {
                JsonDocument.Parse(jsonInput);

                using (var qrGen = new QRCodeGenerator())
                {
                    var qrData = qrGen.CreateQrCode(jsonInput, QRCodeGenerator.ECCLevel.Q);
                    var qrCode = new QRCode(qrData);

                    Bitmap qrCodeImge = qrCode.GetGraphic(10);

                    using (var ms = new MemoryStream())
                    {
                        qrCodeImge.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        return ms.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error Creating QR code: ", ex);
                return new byte[0];
            }
        }

        public BitmapImage GenerateQRCodeAndConvertToBitmapImage(string jsonInput)
        {
            try
            {
                // Generate QR Code image as a byte array
                byte[] qrCodeByteArray = GenerateQRCodeImage(jsonInput);

                if (qrCodeByteArray != null && qrCodeByteArray.Length > 0)
                {
                    // Convert the byte array to a BitmapImage
                    BitmapImage bitmapImage = new BitmapImage();
                    using (MemoryStream stream = new MemoryStream(qrCodeByteArray))
                    {
                        bitmapImage.BeginInit();
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.StreamSource = stream;
                        bitmapImage.EndInit();
                        bitmapImage.Freeze();  // Freezing the image to prevent modifications
                    }
                    return bitmapImage;
                }

                return null; // Return null if QR code generation failed
            }
            catch (Exception ex)
            {
                _logger.Error("Error generating QR code and converting to BitmapImage: " + ex.Message);
                return null;
            }
        }
    }
}
