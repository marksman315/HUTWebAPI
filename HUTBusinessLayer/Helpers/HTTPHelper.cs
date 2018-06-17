using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HUTBusinessLayer.API.Helpers
{
    public static class HTTPHelper
    {
        public static async Task<string> PostValues(Uri uri, object model)
        {
            var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            using (var client = GetHttpClient())
            {
                var httpResponse = await client.PostAsync(uri, httpContent).ConfigureAwait(false);

                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return responseContent.ToString();
                }
            }

            return null;
        }

        public static async Task<string> PostValues(Uri uri, string model, Dictionary<string, string> headers)
        {
            var httpContent = new StringContent(model, Encoding.UTF8, "application/json");

            using (var client = GetHttpClient(headers))
            {                
                var httpResponse = await client.PostAsync(uri, httpContent).ConfigureAwait(false);

                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return responseContent.ToString();
                }
            }

            return null;
        }

        private static HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(@"application/json"));

            return client;
        }

        private static HttpClient GetHttpClient(Dictionary<string, string> customHeaders)
        {            
            return AddCustomHeaders(GetHttpClient(), customHeaders);
        }

        private static HttpClient AddCustomHeaders(HttpClient client, Dictionary<string, string> headers)
        {
            foreach (KeyValuePair<string, string> entry in headers)
            {
                client.DefaultRequestHeaders.Add(entry.Key, entry.Value);
            }

            return client;
        }
    }
}
