using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Exodus3.Domain;
using Newtonsoft.Json;

namespace Exodus3.Core
{
    public class DataService
    {
        public DataService()
        {
        }

        public async Task<IEnumerable<T>> GetRemoteData<T>(DateTimeOffset? since = null) where T: E3Entity
        {
            var retVal = new List<T>();

            string requestUrl = null;

            if (typeof(T) == typeof(Sermon))
            {
                requestUrl = "api/sermons";
            }
            else if (typeof(T) == typeof(Series))
            {
                requestUrl = "api/series";
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(App.BACKEND_URL);

                var a = await client.GetAsync(requestUrl);

                var b = await a.Content.ReadAsStringAsync();
                var s = JsonConvert.DeserializeObject<List<T>>(b);
                retVal = s;
            }

            return retVal;
        }

        public async Task<List<Series>> GetRemoteStuff()
        {
            var retVal = new List<Series>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(App.BACKEND_URL);

                var a = await client.GetAsync("api/series");

                var b = await a.Content.ReadAsStringAsync();
                var s = JsonConvert.DeserializeObject<List<Series>>(b);
                retVal = s;
            }

            return retVal;
        }

    }


}
