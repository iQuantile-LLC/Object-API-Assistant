using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ObjectAPIAssistant
{
    internal static class HttpExtentionLib
    {
        public static async Task<T> ReadAsAsyncJson<T>(this HttpContent content)
        {
            var serializer = new JavaScriptSerializer();
            string x = await content.ReadAsStringAsync();
            return serializer.Deserialize<T>(x);
        }


        public static async Task<HttpResponseMessage> PostAsAsyncJson<T>(this HttpClient client, string uri, T t)
        {
            var serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(t);

            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            return await client.PostAsync(uri, httpContent);
        }


        public static async Task<HttpResponseMessage> PutAsAsyncJson<T>(this HttpClient client, string uri, T t)
        {
            var serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(t);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            return await client.PutAsync(uri, httpContent);
        }

        public static async Task<HttpStatusCode> DeleteAsAsyncJson<T>(this HttpClient client, string uri, string id)
        {
            // var serializer = new JavaScriptSerializer() 
            var response = await client.DeleteAsync(uri + "/" + id);
            return response.StatusCode;

        }
    }
}
