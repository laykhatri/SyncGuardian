using Avalonia.Media.Imaging;
using Net.Codecrete.QrCodeGenerator;
using Serilog;
using SG.Server.Helpers;
using SG.Server.Services.Interfaces;
using System;
using System.IO;

namespace SG.Server.Services.Implementations
{
    public class QRCodeGenerationService : IQRCodeGenerationService
    {
        private readonly ILogger _logger;

        public QRCodeGenerationService(ILogger logger)
        {
            _logger = logger;
        }

        public Bitmap? GenerateQRCodeAndConvertToBitmapImage(string jsonInput)
        {
            try
            {
                var qrCodeByteArray = GenerateQRCodeImage(jsonInput);

                // Check if the byte array is not empty using pattern matching
                if (qrCodeByteArray is { Length: > 0 })
                {
                    // Simplified using statement
                    using var ms = new MemoryStream(qrCodeByteArray);
                    return new Bitmap(ms);
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.Error("Error generating QR code and converting to BitmapImage: " + ex.Message);
                return null;
            }
        }

        public byte[] GenerateQRCodeImage(string jsonInput)
        {
            // Validate the input JSON to ensure it's well-formed.
            if (!jsonInput.IsValidJson())
            {
                _logger.Error("Invalid JSON input.");
                return Array.Empty<byte>();
            }

            try
            {
                var qrCode = QrCode.EncodeText(jsonInput, QrCode.Ecc.Medium);

                return qrCode.ToBmpBitmap();
            }
            catch (Exception ex)
            {
                _logger.Error("Error Creating QR code: ", ex);
                return Array.Empty<byte>();
            }
        }
    }
}
