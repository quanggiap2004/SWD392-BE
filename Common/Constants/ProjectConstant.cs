namespace Common.Constants
{
    public static class ProjectConstant
    {
        public static string STAFF = "Staff";
        public static string USER = "User";
        public static string ADMIN = "Admin";
        public static string VnPay = "VnPay";
        public static string COD = "COD";

        public static string PaymentPending = "Payment Pending";
        public static string PaymentSuccess = "Payment Success";
        public static string PaymentFailed = "Payment Failed";

        public static string RefundAvailable = "Available";
        public static string RefundRequest = "Request";
        public static string RefundResolved = "Resolved";
        public enum OrderStatus
        {
            Pending = 1,
            Processing = 2,
            Shipping = 3,
            Cancelled = 4,
            Arrived = 5
        }
    }
}
