using System;
using System.Collections.Generic;

namespace ShopWeb.Models
{
    public partial class Account
    {
        public Account()
        {
            Carts = new HashSet<Cart>();
        }

        public int Id { get; set; }

        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
    }
}
