namespace Common.Model.OrderDTOs.Request
{
    public class CreateOrderDtoDetail
    {
        public CreateOrderDTO createOrderDto { get; set; }
        public decimal revenue { get; set; }
        public bool openRequest { get; set; }
        public int currentOrderStatusId { get; set; }
        public string paymentStatus { get; set; }
    }
}
