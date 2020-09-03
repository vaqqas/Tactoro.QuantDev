using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tactoro.QuantDev.Models
{
    public partial class UserInfo
    {
        public UserInfo() { }
        public UserInfo(User user)
        {
            this.Alias = user.Alias;
            this.Email = user.Email;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.UserId = user.Id;
            this.UserName = user.UserName;
        }

        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Alias { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public User CreateModel()
        {
            return new User()
            {
                Id = Convert.ToInt32(this.UserId),
                UserName = this.UserName,
                Email = this.Email,
                Alias = this.Alias,
                FirstName = this.FirstName,
                LastName = this.LastName
            };
        }
    }
}
