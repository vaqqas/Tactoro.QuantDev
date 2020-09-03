using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tactoro.QuantDev.Models
{
    public partial class Customer
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Level { get; set; }
        public int? ManagerId { get; set; }

        [JsonIgnore]
        public virtual Manager Manager { get; set; }
        public virtual User User { get; set; }
    }
}
