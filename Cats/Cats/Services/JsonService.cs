using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Autofac.Features.OwnedInstances;
using Cats.API;
using Cats.Models;
using Newtonsoft.Json;

namespace Cats.Services
{

    public class JsonService : IJsonService
    {
        private readonly Func<Owned<HttpClient>> _clientFactory;
        private readonly IAPIConfiguration _config;

        public JsonService(Func<Owned<HttpClient>> clientFactory, IAPIConfiguration config)
        {
            _clientFactory = clientFactory;
            _config = config;
        }
        
        public async Task<List<Person>> GetPeople()
        {
            using (var client = _clientFactory())
            {
                var apiSettings = await _config.GetSettingsAsync();

                var request = new HttpRequestMessage(HttpMethod.Get, new Uri($"{apiSettings.BaseUrl}"));
                
                var response = await client.Value.SendAsync(request);

                var responseString = await response.Content.ReadAsStringAsync();

                try
                {
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception e)
                {
                    throw;
                }

                return JsonConvert.DeserializeObject<List<Person>>(responseString);
            }
        }
    }
}
