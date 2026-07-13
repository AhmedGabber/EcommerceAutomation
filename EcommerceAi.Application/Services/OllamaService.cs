using EcommerceAi.Application.Dtos.ProductDtos;
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

        public async Task<List<string>> ExtractKeywordsAsync(string message)
        {
            var prompt =
            $$"""
You are an ecommerce keyword extractor.

Your task is to extract search keywords from the user's message.

Return ONLY valid JSON.

The JSON MUST exactly follow this schema:

{
  "keywords": [
    "keyword1",
    "keyword2",
    "keyword3"
  ]
}

Rules:
- Do not return objects.
- Do not return explanations.
- Do not return markdown.
- Do not return extra text.
- Do not invent products.
- Return at most 5 keywords.
- Keep only important search terms.
- If there are no keywords return:

{
  "keywords": []
}

User Message:
{{message}}

JSON:
""";

            var response = await AskAsync(prompt);

            var result = JsonSerializer.Deserialize<ProductSearchKeywordsDto>(
                response,
                new JsonSerializerOptions
                {

                    PropertyNameCaseInsensitive = true
                });

            return result?.Keywords ?? [];
        }


    }
}
