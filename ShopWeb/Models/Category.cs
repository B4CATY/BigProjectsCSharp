using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ShopWeb.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        [DisplayName("Category Name")]
        public string? Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
