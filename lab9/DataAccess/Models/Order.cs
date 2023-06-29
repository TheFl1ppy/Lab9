using System;
using System.Collections.Generic;

namespace BackendApi.Models
{
    public partial class Order
    {
        public int IdUser { get; set; }
        public int OrderId { get; set; }
        public int DeliveryId { get; set; }
        public int StatusId { get; set; }
        public DateTime Date { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Delivery Delivery { get; set; } = null!;
        public virtual User IdUserNavigation { get; set; } = null!;
        public virtual Status Status { get; set; } = null!;
    }
}
