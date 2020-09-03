using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tactoro.QuantDev.Models
{
    public partial class User
    {
        public User()
        {
            Customer = new HashSet<Customer>();
            Manager = new HashSet<Manager>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Alias { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Customer> Customer { get; set; }

        [JsonIgnore]
        public virtual ICollection<Manager> Manager { get; set; }
    }
}
