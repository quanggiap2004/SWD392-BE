using Application.Services.Interfaces;
using Common.Constants;
using Common.Model.OrderStatusDetailDTOs;
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
        private readonly IOrderService _orderService;
        private readonly IOrderStatusDetailService _orderStatusDetailService;

        public ShippingService(HttpClient httpClient, IOrderService orderService, IOrderStatusDetailService orderStatusDetailService)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("Token", API_TOKEN);
            _httpClient.DefaultRequestHeaders.Add("ShopId", SHOP_ID);
            _orderService = orderService;
            _orderStatusDetailService = orderStatusDetailService;
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

        public async Task<bool> UpdateOrderStatusForShipping(int orderId)
        {
            var result = await _orderService.UpdateOrderForShipping(orderId);
            if(result == false)
            {
                throw new Exception("Update order current status failed");
            }
            var updateStatusShipping = await _orderStatusDetailService.AddOrderStatusDetailAsync(new OrderStatusDetailSimple
            {
                orderId = orderId,
                note = "Change to shipping status",
                statusId = (int)ProjectConstant.OrderStatus.Shipping,
                updatedAt = DateTime.UtcNow,
            });
            if (updateStatusShipping == false)
            {
                throw new Exception("Update status shipping failed");
            }
            var updateStatusArrived = await _orderStatusDetailService.AddOrderStatusDetailAsync(new OrderStatusDetailSimple
            {
                orderId = orderId,
                note = "Change to arrived status",
                statusId = (int)ProjectConstant.OrderStatus.Arrived,
                updatedAt = DateTime.UtcNow.AddDays(2),
            });
            if(updateStatusArrived == false || updateStatusShipping == false)
            {
                throw new Exception("Update status arrived failed");
            }
            return true;
        }
    }
}
