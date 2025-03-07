namespace Common.Model.AuthenticationDTO
{
    public class RegisterAccountDTO
    {
        public string email { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string fullName { get; set; }
        public string phoneNumber { get; set; }
        public bool gender { get; set; }
        public int roleId { get; set; }
        public bool isTestAccount { get; set; }
    }
}
