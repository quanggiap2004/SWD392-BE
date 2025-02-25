namespace Domain.Domain.Model.AuthenticationDTO
{
    public class ChangePasswordDto
    {
        public string email { get; set; }
        public string currentPassword { get; set; }
        public string newPassword { get; set; }
    }
}
