using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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

        public async Task<int> GetLatestVersion()
        {
            var request = new GameforgeRequest<int>(HttpMethod.Get, "/patching/download/nostale/default?branchToken");
            Dictionary<string, int> response = await request.Send();

            return response.GetValueOrDefault("latest");
        }

        public async Task<AuthorizedGameforgeApi> Login(string email, string password, Locales locale, Guid? installationId = null)
        {
            if (installationId == null)
            {
                installationId = GenerateIntallationId(email, password);
            }
            
            var request = new GameforgeRequest<AuthRequest, string>(HttpMethod.Post, "/auth/sessions", installationId.Value);
            
            var authRequest = new AuthRequest
            {
                Locale = locale.Value,
                Email = email,
                Password = password
            };

            Dictionary<string, string> response = await request.Send(authRequest);
            string authToken = response.GetValueOrDefault("token") ?? string.Empty;
            
            return new AuthorizedGameforgeApi(authToken, installationId.Value);
        }
    }
}
