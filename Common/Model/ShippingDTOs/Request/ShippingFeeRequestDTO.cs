namespace Common.Model.ShippingDTOs.Request
{
    public class ShippingFeeRequestDTO
    {
        public int service_id { get; set; }
        public int to_district_id { get; set; }
        public int weight { get; set; }
    }
}
