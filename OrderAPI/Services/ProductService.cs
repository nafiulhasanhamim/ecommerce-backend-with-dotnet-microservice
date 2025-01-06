using OrderAPI.DTOs;
using OrderAPI.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace OrderAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductService(IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
        }
        public async Task<OrderResultDto> ProcessOrderAsync(List<OrderedProductCreateDto> orderItems)
        {
            var client = _httpClientFactory.CreateClient("Product");

            // Serialize order items into JSON
            var content = new StringContent(
                JsonConvert.SerializeObject(orderItems),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync("/api/Product/process", content);

            if (response.IsSuccessStatusCode)
            {
                var apiContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<OrderResultDto>(apiContent);
            }

            throw new Exception($"Failed to process order. Status Code: {response.StatusCode}");
        }

    }
}