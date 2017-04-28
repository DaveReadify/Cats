using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cats.API
{
    public class APIConfiguration : IAPIConfiguration
    {
        private APISettings _settings;

        public async Task<APISettings> GetSettingsAsync()
        {
            if (_settings != null)
                return _settings;

            var storageFile = typeof(APIConfiguration).GetTypeInfo()
                    .Assembly.GetManifestResourceStream("Cats.apisettings.json");

            using (var reader = new System.IO.StreamReader(storageFile))
            {
                var settingsText = await reader.ReadToEndAsync();
                _settings = JsonConvert.DeserializeObject<APISettings>(settingsText);
            }

            return _settings;
        }
    }
}
