using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tactoro.QuantDev.Models
{
    public partial class Manager
    {
        public Manager()
        {
            Customers = new HashSet<Customer>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Position { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
