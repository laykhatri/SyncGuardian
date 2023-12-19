namespace SyncGuardianMobile.Services.Interface
{
    public interface ISharedPrefs
    {
        void SetValue<T>(string key, T value);
        T GetValue<T>(string key);
    }
}
