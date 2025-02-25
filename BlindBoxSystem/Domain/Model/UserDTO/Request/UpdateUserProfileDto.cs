﻿namespace BlindBoxSystem.Domain.Model.UserDTO.Request
{
    public class UpdateUserProfileDto
    {
        public int userId { get; set; }
        public string username { get; set; }
        public string fullname { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public Boolean gender { get; set; }
    }
}
