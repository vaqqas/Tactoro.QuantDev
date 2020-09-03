using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tactoro.QuantDev.Models
{
    public partial class CustomerInfo : UserInfo
    {
        public CustomerInfo() { }
        public CustomerInfo(Customer customer): base(customer.User)
        {
            this.CustomerId = customer.Id;
            this.ManagerId = customer.ManagerId;
            this.Level = customer.Level;
        }

        public int? CustomerId { get; set; }
        public int? ManagerId { get; set; }
        public string Level { get; set; }

        public new Customer CreateModel()
        {
            return new Customer()
            {
                Id = Convert.ToInt32(this.CustomerId),
                ManagerId = Convert.ToInt32(this.ManagerId),
                Level = this.Level,
                User = base.CreateModel()
            };
        }
    }
}
