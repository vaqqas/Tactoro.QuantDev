using System;
using System.Collections.Generic;

namespace Tactoro.QuantDev.Models
{
    /// <summary>
    /// Poco object to share Manager user information via JSON
    /// </summary>
    public partial class ManagerInfo : UserInfo
    {
        public ManagerInfo() { }
        public ManagerInfo(Manager manager) : base(manager.User)
        {
            this.ManagerId = manager.Id;
            this.Position = manager.Position;
        }

        public int? ManagerId { get; set; }
        public string Position { get; set; }

        public new Manager CreateModel()
        {
            return new Manager() { 
                Id = Convert.ToInt32(this.ManagerId),
                Position = this.Position,
                User = base.CreateModel()
            };
        }
    }
}
