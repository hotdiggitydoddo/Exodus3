using System.Threading.Tasks;

namespace Exodus3.Services
{
    public class OldDataService
    {
     
        public OldDataService()
        {
            //BaseUrl = App.BACKEND_URL;
          //  base.BaseUrl = "s";
        }

        public async Task Sync()
        {
            //var req = new RestRequest("sermons/1", HttpMethod.Get);
           // client.BaseAddress = new Uri(App.BACKEND_URL);
         //   client.DefaultRequestHeaders.Accept.Clear();
         //   client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));



            // ... Target page.
            string page = "http://en.wikipedia.org/";

            // ... Use HttpClient.
            //using (HttpClient client = new HttpClient())
            //using (HttpResponseMessage response = await client.GetAsync(page))
            //using (HttpContent content = response.Content)
            //{
            //    // ... Read the string.
            //    string result = await content.ReadAsStringAsync();

            //    // ... Display the result.
            //    if (result != null &&
            //        result.Length >= 50)
            //    {
            //      //  System.Console.WriteLine(result.Substring(0, 50) + "...");
            //    }
            //}

            //try
            //{
            //    using (var client = new HttpClient())
            //    {
            //        var response = await "http://localhost:5000/api/sermons/2".GetJsonAsync<Sermon>();
            //        //response.EnsureSuccessStatusCode();
            //        //var product = await response.Content.ReadAsAsync<Sermon>();
            //        var a = response;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    var a = ex;
            //}



        }
    }
}
