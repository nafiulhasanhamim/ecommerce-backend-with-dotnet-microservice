using OrderAPI.DTOs;
using OrderAPI.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace OrderAPI.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PaymentService(IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
        }
        public async Task<PaymentResponseDTO> GetBkashUrlAsync(PaymentDTO paymentData)
        {
            var client = _httpClientFactory.CreateClient("Payment");

            // Serialize order items into JSON
            var content = new StringContent(
                JsonConvert.SerializeObject(paymentData),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync("/api/bkash/payment/create", content);

            if (response.IsSuccessStatusCode)
            {
                var apiContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PaymentResponseDTO>(apiContent);
            }

            throw new Exception($"Failed to process order. Status Code: {response.StatusCode}");
        }

    }
}