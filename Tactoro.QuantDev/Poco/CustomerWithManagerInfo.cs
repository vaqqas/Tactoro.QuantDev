using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tactoro.QuantDev.Models
{
    public partial class CustomerWithManagerInfo : CustomerInfo
    {
        public CustomerWithManagerInfo() { }
        public CustomerWithManagerInfo(Customer customer) : base(customer)
        {
            this.Manager = new ManagerInfo(customer.Manager);
        }

        public ManagerInfo Manager { get; set; }
    }
}
