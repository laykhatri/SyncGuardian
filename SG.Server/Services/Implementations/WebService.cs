using SG.Server.Services.Interfaces;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace SG.Server.Services.Implementations
{
    internal class WebService : IWebService
    {
        private Process? _process;
        public void Start()
        {
            var path = GetWebApiProjectPath();

            // Initialize the Property with new Process information
            _process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = $"run --project {path}",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            // Start the process
            _process.Start();
        }

        public void Stop()
        {
            // Kill the process if available
            _process?.Kill();
        }

        private static string GetWebApiProjectPath()
        {
            // Get the full path to the directory containing the executing assembly
            var assemblyPath = Assembly.GetExecutingAssembly().Location;
            var directoryPath = Path.GetDirectoryName(assemblyPath);

            // Assuming the Web API project is in a known relative path from the executing assembly
            // Adjust the relative path to point to your Web API project's directory
            var relativePathToWebApiProject = @"..\..\..\..\SG.WebAPI";

            // Combine the directory path with the relative path to get the absolute path
            var webApiProjectPath = Path.GetFullPath(Path.Combine(directoryPath!, relativePathToWebApiProject));

            // Return the path to the .csproj file
            return Path.Combine(webApiProjectPath, "SG.WebAPI.csproj");
        }
    }
}
