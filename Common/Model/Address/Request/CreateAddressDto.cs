namespace Common.Model.Address.Request
{
    public class CreateAddressDto
    {
        public string phoneNumber { get; set; }
        public string name { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int WardCode { get; set; }
        public string AddressDetail { get; set; }
        public int UserId { get; set; }
        public string? note { get; set; }
    }
}
