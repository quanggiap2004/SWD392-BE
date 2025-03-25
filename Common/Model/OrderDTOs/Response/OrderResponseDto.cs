namespace Common.Model.OrderDTOs.Response
{
    public class OrderResponseDto
    {
        public int orderId { get; set; }
        public int userId { get; set; }
        public DateTime orderCreatedAt { get; set; }
        public string paymentMethod { get; set; }
        public decimal totalPrice { get; set; }
        public decimal revenue { get; set; }
        public int? addressId { get; set; }
        public int currentOrderStatusId { get; set; }
        public string url { get; set; }
        public decimal shippingFee { get; set; }
        public decimal subTotal { get; set; }
        public decimal discountAmount { get; set; }
        public bool isReadyForShip { get; set; }
    }
}
