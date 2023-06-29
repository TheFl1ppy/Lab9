using System;
using System.Collections.Generic;

namespace BackendApi.Models
{
    public partial class Status
    {
        public Status()
        {
            Orders = new HashSet<Order>();
        }

        public int StatusId { get; set; }
        public string StatusType { get; set; } = null!;
        public bool IsDeleted { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
