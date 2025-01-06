using Newtonsoft.Json;
using OrderAPI.DTOs;
using OrderAPI.Interfaces;

namespace OrderAPI.Services
{
    public class CouponService : ICouponService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CouponService(IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
        }
        public async Task<CouponReadDto?> GetCouponByNameAsync(string couponName)
        {
            var client = _httpClientFactory.CreateClient("Coupon");
            var response = await client.GetAsync($"/api/coupons/get-by-name/{couponName}");

            var apiContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CouponReadDto?>(apiContent);
        }
    }
}