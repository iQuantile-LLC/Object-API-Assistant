using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ObjectAPIAssistant
{
    public class ApiAssistant<T>
    {
        private string BaseUrl { get; }

        /// <summary>
        /// Change this RequestUri before any Function call
        /// </summary>
        public string RequestUri { get; set; }

        private HttpClient Client { get; }

        /// <summary>
        /// Api Assistant was made to do API calls using Objects instead of JSON
        /// Pass your Template class when you call the constructor.
        /// GET,POST,PUT,DELETE Methods will return you the Objects Directly
        /// You can also get HTTP Response Messages in case you want to handle Status Codes manually.
        /// </summary>
        /// <param name="baseUrl">Pass your base server url</param>
        public ApiAssistant(string baseUrl)
        {
            BaseUrl = baseUrl;
            Client=new HttpClient();
        }


        #region Get
        /// <summary>
        ///  Update the Request URI and call
        /// </summary>
        /// <returns>T</returns>
        public async Task<T> GetObjectAsync()
        {

            T t = default(T);

            HttpResponseMessage response = await Client.GetAsync(BaseUrl + RequestUri);
            if (response.IsSuccessStatusCode)
            {
                t = await response.Content.ReadAsAsyncJson<T>();

            }
            return t;
        }

        /// <summary>
        /// Update the Request URI and call
        /// </summary>
        /// <returns>HTTP Response Message</returns>
        public async Task<HttpResponseMessage> GetResponseAsync()
        {
            return await Client.GetAsync(BaseUrl + RequestUri);
        }

        #endregion

        #region Post

        /// <summary>
        /// Pass your Object here 
        /// Don't Forget to update the Request URI
        /// </summary>
        /// <param name="t">Takes T as input</param>
        /// <returns>HTTP Response Message</returns>
        public async Task<HttpResponseMessage> CreateObjectResponseAsync(T t)
        {
            HttpResponseMessage response = await Client.PostAsAsyncJson(
                BaseUrl + RequestUri, t);

            return response;
        }

        /// <summary>
        /// Pass your Object here 
        /// Don't Forget to update Request URI
        /// </summary>
        /// <param name="t"></param>
        /// <returns>T</returns>
        public async Task<T> CreateObjectAsync(T t)
        {
            HttpResponseMessage response = await Client.PostAsAsyncJson(
                BaseUrl + RequestUri, t);

            return await response.Content.ReadAsAsyncJson<T>();
        }

        /// <summary>
        /// This Method will upload file to server
        /// Feed the fully qualified Filepath to the parameter
        /// Ex. FileName from File Dialogue.
        /// </summary>
        /// <param name="filePath">Fully Qualified File Name</param>
        /// <returns>HTTP Response Message</returns>
        public async Task<HttpResponseMessage> UploadFileAsByteArray(string filePath)
        {
            byte[] fileBytes = File.ReadAllBytes(filePath);
            ByteArrayContent file=new ByteArrayContent(fileBytes);
            return await Client.PostAsync(BaseUrl + RequestUri, file);
        }




        #endregion

        #region Put

        /// <summary>
        /// Pass your Object here 
        /// Don't Forget to update Request URI
        /// </summary>
        /// <param name="t"></param>
        /// <returns>T</returns>
        public async Task<T> UpdateObjectAsync(T t)
        {
            HttpResponseMessage response = await Client.PutAsAsyncJson(BaseUrl + RequestUri, t);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsyncJson<T>();

        }


        /// <summary>
        /// Pass your Object here 
        /// Don't Forget to update Request URI
        /// </summary>
        /// <param name="t"></param>
        /// <returns>HTTP Response Message</returns>
        public async Task<HttpResponseMessage> UpdateObjectResponseAsync(T t)
        {
            return await Client.PutAsAsyncJson(BaseUrl + RequestUri, t);
         
        }


        #endregion

        #region Delete

        /// <summary>
        /// Pass your Object here 
        /// Don't Forget to update Request URI
        /// </summary>
        /// <param name="id">Pass object id</param>
        /// <returns>Status Code</returns>
        public async Task<HttpStatusCode> DeleteObjecAsync(string id)
        {
            return await Client.DeleteAsAsyncJson<T>(BaseUrl + RequestUri, id);
        }

        #endregion
    }
}
