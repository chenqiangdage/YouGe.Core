using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace YouGe.Core.Commons
{
    public static class HttpClientExtensions
    {
        public static async Task<T> PostJsonAsync<T>(this HttpClient client, string url, object data) where T : class, new()
        {
            try
            {

                string content = JsonConvert.SerializeObject(data);
                var buffer = Encoding.UTF8.GetBytes(content);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PostAsync(url, byteContent).ConfigureAwait(false);
                string result = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return new T();
                }
                return JsonConvert.DeserializeObject<T>(result);
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    string responseContent = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                    throw new System.Exception($"response :{responseContent}", ex);
                }
                throw;
            }
        }

        public static async Task<string> GetJsonAsync(this HttpClient client, Dictionary<string, string> parameters, string url)
        {
            if (parameters != null)
            {
                var strParam = string.Join("&", parameters.Select(o => o.Key + "=" + o.Value));
                url = string.Concat(ConcatURL(url), '?', strParam);
            }
            else
            {
                url = ConcatURL(url);
            }

            string result = await client.GetStringAsync(url);
            return result;
        }

        private static string ConcatURL(string requestUrl)
        {
            return new Uri(requestUrl).OriginalString;
        }
    }
}
