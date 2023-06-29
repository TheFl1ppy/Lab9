using System;
using System.Collections.Generic;

namespace BackendApi.Models
{
    public partial class User
    {


        public User()
        {
            Carts = new HashSet<Cart>();
            Orders = new HashSet<Order>();
        }

        public int id_user { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int RoleId { get; set; }
        public string Address { get; set; } = null!;
        public bool IsDeleted { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}
