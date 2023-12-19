namespace SyncGuardianMobile.Services.Interface
{
    public interface IDeviceIDGenerator
    {
        string ComputeHash(string inputString);
        string GetCPUDetails();
        string GetFingerPrint();
        string GetODMSKU();
        string GetSocManufacturer();
        string GenerateConcatedInfo(string cpu, string fingerprint, string odmSku, string socManufacturer, string guid);
        string GenerateUniqueGUID();
        bool SaveGuidToPrefs(string guid);
        string ReadGuidFromPrefs();
        bool NeedInitialSetup();
        string GenerateDeviceId();
        bool ValidateDeviceId(string hash);
        bool ResetDeviceId();
    }
}
