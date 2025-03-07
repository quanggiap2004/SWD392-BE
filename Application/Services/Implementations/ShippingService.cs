using Application.Services.Interfaces;
using Common.Model.ShippingDTOs.Request;
using Common.Model.ShippingDTOs.Response;
using System.Text;
using System.Text.Json;

namespace Application.Services.Implementations
{
    public class ShippingService : IShippingService
    {

        private readonly HttpClient _httpClient;
        private const string API_TOKEN = "62417330-f6d2-11ef-91ea-021c91d80158";
        private const string BASE_URL = "https://online-gateway.ghn.vn/shiip/public-api/v2";
        private const string SHOP_ID = "5662788";

        public ShippingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("Token", API_TOKEN);
            _httpClient.DefaultRequestHeaders.Add("ShopId", SHOP_ID);
        }

        public async Task<string> GetShopsAsync()
        {
            var response = await _httpClient.GetAsync($"{BASE_URL}/shop/all");
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<ShippingFeeResponseDTO> GetShippingFeeAsync(ShippingFeeRequestDTO shippingFeeRequest)
        {

            string url = $"{BASE_URL}/shipping-order/fee";
            var jsonContent = JsonSerializer.Serialize(shippingFeeRequest);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var shippingFeeResponse = JsonSerializer.Deserialize<ShippingFeeResponseDTO>(responseContent);
                return shippingFeeResponse;

            }

            throw new Exception($"GHTK API Error: {response.StatusCode}");
        }


    }
}
