namespace BlindBoxSystem.Domain.Model.UserDTO.Response
{
    public class UserLoginResponse
    {
        public string username { get; set; }
        public string fullname { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public Boolean gender { get; set; }
        public int roleId { get; set; }
    }
}
