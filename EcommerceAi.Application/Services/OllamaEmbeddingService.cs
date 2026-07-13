using EcommerceAi.Application.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Application.Services
{
    public class OllamaEmbeddingService : IEmbeddingService
    {
        private readonly HttpClient _httpClient;

        public OllamaEmbeddingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<float[]> GenerateEmbeddingAsync(string text)
        {
            var request = new
            {
                model = "nomic-embed-text",
                prompt = text
            };


            var response = await _httpClient.PostAsJsonAsync(
                "http://localhost:11434/api/embeddings",
                request);


            response.EnsureSuccessStatusCode();


            var result = await response.Content
                .ReadFromJsonAsync<OllamaEmbeddingResponse>();


            return result!.Embedding;
        }
    }


    public class OllamaEmbeddingResponse
    {
        public float[] Embedding { get; set; }
    }
}
