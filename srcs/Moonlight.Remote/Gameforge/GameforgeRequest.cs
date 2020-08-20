using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Moonlight.Remote.Gameforge
{
    public class GameforgeRequest
    {
        protected const string Url = "https://spark.gameforge.com/api/v1";
        protected static string UserAgent = "GameforgeClient/2.1.12";
        protected const string MediaType = "application/json";

        public static void SetVersion(string version)
        {
            if (!string.IsNullOrEmpty(version))
            {
                UserAgent = "GameforgeClient/" + version;
            }
        }   
    }
    
    public class GameforgeRequest<T> : GameforgeRequest
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public GameforgeRequest(HttpMethod method, string path, Guid? installationId, string bearerToken)
        {
            Method = method;
            Path = path;
            InstallationId = installationId;
            BearerToken = bearerToken;
        }

        public GameforgeRequest(HttpMethod method, string path, Guid? installationId)
            : this(method, path, installationId, null)
        {
        }
        
        public GameforgeRequest(HttpMethod method, string path)
            : this(method, path, null, null)
        {
        }

        public string Path { get; set; }

        public Guid? InstallationId { get; set; }
        public HttpMethod Method { get; set; }

        public string BearerToken { get; set; }

        public async Task<Dictionary<string, T>> Send()
        {
            using (HttpRequestMessage request = PrepareRequest())
            {
                return await GetResponse(request);
            }
        }

        protected async Task<Dictionary<string, T>> GetResponse(HttpRequestMessage request)
        {
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Dictionary<string, T>>(content);
        }

        protected HttpRequestMessage PrepareRequest()
        {
            var request = new HttpRequestMessage(Method, $"{Url}" + Path);
            if (InstallationId != null)
            {
                request.Headers.Add("TNT-Installation-Id", InstallationId.ToString());
            }

            request.Headers.Add("User-Agent", UserAgent);

            if (BearerToken != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);
            }

            return request;
        }
    }

    public class GameforgeRequest<T, TU> : GameforgeRequest<TU>
    {
        public GameforgeRequest(HttpMethod method, string path, Guid installationId, string bearerToken = null)
            : base(method, path, installationId, bearerToken)
        {
        }

        public async Task<Dictionary<string, TU>> Send(T body)
        {
            using (HttpRequestMessage request = PrepareRequest())
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, MediaType);
                return await GetResponse(request);
            }
        }
    }
}