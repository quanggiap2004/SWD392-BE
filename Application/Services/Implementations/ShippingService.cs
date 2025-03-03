using Application.Services.Interfaces;

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

        public async Task<string> GetShippingFeeAsync(string pickProvince, string pickDistrict, string province, string district, int weight, int value)
        {

            string url = $"{BASE_URL}/fee?pick_province={pickProvince}&pick_district={pickDistrict}&province={province}&district={district}&weight={weight}&value={value}";

            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            throw new Exception($"GHTK API Error: {response.StatusCode}");
        }


    }
}
