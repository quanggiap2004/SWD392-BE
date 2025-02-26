namespace Domain.Domain.Model.Address.Request
{
    public class CreateAddressDto
    {
        public string phoneNumber { get; set; }
        public string name { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public string AddressDetail { get; set; }
        public int UserId { get; set; }
    }
}
