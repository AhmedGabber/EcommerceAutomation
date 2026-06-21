using EcommerceAi.Application.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EcommerceAi.Application.Services
{
    public class OllamaService : IOllamaService
    {
        private readonly HttpClient _httpClient;

        public OllamaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> AskAsync(string prompt)
        {
            var request = new
            {
                model = "phi3:mini",
                prompt = prompt,
                stream = false
            };

            var response =
                await _httpClient.PostAsJsonAsync(
                    "/api/generate",
                    request);

            response.EnsureSuccessStatusCode();

            var json =
                await response.Content.ReadFromJsonAsync<JsonElement>();

            return json
                .GetProperty("response")
                .GetString()!;
        }
    }
}
