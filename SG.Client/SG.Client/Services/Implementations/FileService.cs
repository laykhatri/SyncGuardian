using SG.Client.Services.Interfaces;
using Splat;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SG.Client.Services.Implementations
{
    public class FileService : IFileService
    {
        private readonly ILogger _logger;

        public FileService(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<string> ReadTextAsync(string filePath)
        {
            try
            {
                _logger.Write($"Starting File read for: {filePath}", LogLevel.Info);
                var data = await File.ReadAllTextAsync(filePath);
                _logger.Write($"File read success for: {filePath}", LogLevel.Info);
                return data;

            }
            catch (Exception ex)
            {
                _logger.Write(ex, ex.Message, LogLevel.Error);
                return string.Empty;
            }
        }

        public async Task WriteTextAsync(string filePath, string content)
        {
            try
            {
                _logger.Write($"Starting File write for: {filePath}", LogLevel.Info);
                await File.WriteAllTextAsync(filePath, content);
                _logger.Write($"File write success for: {filePath}", LogLevel.Info);
            }
            catch (Exception ex)
            {
                _logger.Write(ex, ex.Message, LogLevel.Error);
            }
        }
    }
}
