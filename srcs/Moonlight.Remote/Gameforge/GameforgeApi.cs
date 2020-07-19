using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Moonlight.Remote.Extensions;
using Newtonsoft.Json;

namespace Moonlight.Remote.Gameforge
{
    public class GameforgeApi
    {
        private const string URL = "https://spark.gameforge.com/api/v1";
        private const string USER_AGENT = "GameforgeClient/2.1.11";
        private const string MEDIA_TYPE = "application/json";

        private readonly HttpClient _httpClient;

        public GameforgeApi()
        {
            _httpClient = new HttpClient();
        }

        public Guid GenerateIntallationId(string email, string password)
        {
            return Guid.Parse(Cryptography.Cryptography.ToMd5(email + password));
        }

        public async Task<string> GetAuthToken(string email, string password, string locale, Guid installationId)
        {
            string json = JsonConvert.SerializeObject(new AuthRequest
            {
                Locale = locale,
                Email = email,
                Password = password
            });

            using (var request = new HttpRequestMessage(HttpMethod.Post, $"{URL}/auth/sessions")
            {
                Content = new StringContent(json, Encoding.UTF8, MEDIA_TYPE)
            })
            {
                request.Headers.Add("TNT-Installation-Id", installationId.ToString());

                HttpResponseMessage response = await _httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    return string.Empty;
                }

                string content = await response.Content.ReadAsStringAsync();
                Dictionary<string, string> jsonContent = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);

                return jsonContent.GetValueOrDefault("token") ?? string.Empty;
            }
        }

        public async Task<IEnumerable<GameforgeAccount>> GetAccounts(string token, Guid installationId)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"{URL}/user/accounts"))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                request.Headers.Add("User-Agent", USER_AGENT);
                request.Headers.Add("TNT-Installation-Id", installationId.ToString());

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    return new List<GameforgeAccount>();
                }

                string content = await response.Content.ReadAsStringAsync();
                Dictionary<string, GameforgeAccount> jsonContent = JsonConvert.DeserializeObject<Dictionary<string, GameforgeAccount>>(content);

                return jsonContent?.Values.ToArray() ?? Array.Empty<GameforgeAccount>();
            }
        }

        public async Task<string> GetSessionToken(string token, GameforgeAccount gameforgeAccount, Guid installationId)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, $"{URL}/auth/thin/codes"))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                request.Headers.Add("User-Agent", USER_AGENT);
                request.Headers.Add("TNT-Installation-Id", installationId.ToString());

                string json = JsonConvert.SerializeObject(new SessionRequest
                {
                    PlatformGameAccountId = gameforgeAccount.Id
                });

                request.Content = new StringContent(json, Encoding.UTF8, MEDIA_TYPE);

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    return string.Empty;
                }

                string content = await response.Content.ReadAsStringAsync();
                Dictionary<string, string> jsonContent = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);

                return (jsonContent.GetValueOrDefault("code") ?? string.Empty).ToHex();
            }
        }
    }
}
