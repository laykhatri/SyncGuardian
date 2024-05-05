namespace SG.Server.Services.Interfaces
{
    public interface IDeviceIDGenerationService
    {
        // Registry Methods
        bool SaveGUIDToRegistry(string guid);
        string ReadGUIDFromRegistry();

        // Get OS Version
        string GetOSVersion();

        // Hardware information
        string GetCPUDetails();
        string GetGPUDetails();

        // Device ID methods
        string GenerateDeviceId();
        bool ValidateDeviceID(string hash);

        // Helper Methods
        string GenerateUniqueGUID();
        string GenerateConcatedInfo(string cpu, string gpu, string windowsVersion, string guid);
        bool NeedInitialSetup();
    }
}
