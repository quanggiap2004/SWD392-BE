﻿using Common.Model.OrderItem.Request;

namespace Common.Model.OrderDTOs.Request
{
    public class CreateOrderDTO
    {
        public int userId { get; set; }
        public int? orderId { get; set; }
        public decimal totalPrice { get; set; }
        public decimal shippingFee { get; set; }
        public int? voucherId { get; set; }
        public string paymentMethod { get; set; }
        public int? addressId { get; set; }
        public decimal subTotal { get; set; }
        public decimal discountAmount { get; set; }
        public bool isShipBoxItem { get; set; } = false;
        public ICollection<OrderItemRequestDto> orderItemRequestDto { get; set; }
    }
}
