namespace BlindBoxSystem.Domain.Model.Address.Response
{
    public class AddressResponseDto
    {
        public int addressId { get; set; }
        public string province { get; set; }
        public string district { get; set; }
        public string ward { get; set; }
        public string addressDetail { get; set; }
        public int userId { get; set; }
    }
}
