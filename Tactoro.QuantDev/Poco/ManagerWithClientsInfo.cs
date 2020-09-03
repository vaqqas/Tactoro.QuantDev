using System;
using System.Collections.Generic;

namespace Tactoro.QuantDev.Models
{
    /// <summary>
    /// Poco object to share Manager user information via JSON
    /// </summary>
    public class ManagerWithClientsInfo : ManagerInfo
    {
        public ManagerWithClientsInfo() { }
        public ManagerWithClientsInfo(Manager manager): base(manager)
        {
            this.Clients = new List<CustomerInfo>();
            foreach(var customer in manager.Customers)
            {
                this.Clients.Add(new CustomerInfo(customer));
            }
        }

        public List<CustomerInfo> Clients{ get; set; }
    }
}
