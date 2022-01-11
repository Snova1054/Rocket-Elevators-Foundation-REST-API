using System;

namespace RocketElevatorsRESTAPI.Models
{
    public class User
    {
        public int id { get; set; }
        public string email { get; set; }
        public string encrypted_password { get; set; }
        public string reset_password_token { get; set; }
        public DateTime? reset_password_sent_at { get; set; }
        public DateTime? remember_created_at { get; set; }
        public int? admin { get; set; }
        public int? employee { get; set; }
        public int? client { get; set; }
    }
}