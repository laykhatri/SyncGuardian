using QRCoder;
using Serilog;
using System;
using System.Drawing;
using System.IO;
using System.Text.Json;

namespace SyncGuardianWpf.Services.Interfaces
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

                    Bitmap qrCodeImge = qrCode.GetGraphic(512);

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
    }
}
