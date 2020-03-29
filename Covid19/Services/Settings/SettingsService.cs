using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Covid19.Services
{
    public class SettingsService: ISettingsService
    {
        IDictionary<string, object> Settings => Application.Current.Properties;

        public async Task Save()
        {
            await Application.Current.SavePropertiesAsync();
        }

        public T Get<T>(string name)
        {
            if (Settings.ContainsKey(name))
                return (T)Settings[name];

            return default;
        }

        public void Set<T>(string name, T value)
        {
            Settings[name] = value;
        }

        public bool Delete(string name)
        {
            if (IsHaving(name))
                return Settings.Remove(name);

            return false;
        }

        public bool IsHaving(string name)
        {
            return Settings.ContainsKey(name);
        }
    }
}
