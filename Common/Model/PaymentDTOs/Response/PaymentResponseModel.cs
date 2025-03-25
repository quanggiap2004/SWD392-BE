namespace Common.Model.PaymentDTOs.Response
{
    public class PaymentResponseModel
    {
        public string TransactionId { get; set; }
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentId { get; set; }
        public bool Success { get; set; }
        public string Token { get; set; }
        public string VnPayResponseCode { get; set; }
        public decimal totalPrice { get; set; }

        public DateOnly PaymentDate { get; set; }
    }
}
