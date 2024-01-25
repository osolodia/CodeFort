using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CodeFort.Storage
{
    public class SecureStorageManager
    {
        public static string UserNameKey = "user_name";
        public static string PasswordKey = "user_password";
        public async static Task<string?> GetSavedUserName()
        {
            return await SecureStorage.GetAsync(UserNameKey);
        }
        public async static Task<string?> GetSavedPassword()
        {
            return await SecureStorage.GetAsync(PasswordKey);
        }
        public async static Task<bool> SetSavedData(string userName, string password)
        {
            try
            {
                await SecureStorage.SetAsync(UserNameKey, userName);
                await SecureStorage.SetAsync(PasswordKey, password);
                return true;
            }
            catch
            {
                RemoveSavedData();
                return false;
            }
        }
        public static void RemoveSavedData()
        {
            SecureStorage.Remove(UserNameKey);
            SecureStorage.Remove(PasswordKey);
        }
    }
}