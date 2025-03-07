namespace Common.Model.Address.Response
{
    public class AddressResponseDto
    {
        public int? addressId { get; set; }
        public string province { get; set; }
        public string district { get; set; }
        public string ward { get; set; }
        public int provinceId { get; set; }
        public int districtId { get; set; }
        public int wardCode { get; set; }
        public string addressDetail { get; set; }
        public int userId { get; set; }
        public string phoneNumber { get; set; }
        public string name { get; set; }
        public string? note { get; set; }
    }
}
