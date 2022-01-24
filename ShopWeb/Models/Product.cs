using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ShopWeb.Models
{
    public partial class Product
    {
        public Product()
        {
            CardProducts = new HashSet<CardProduct>();
        }

        public int Id { get; set; }
        public int? Categoryid { get; set; }
        [DisplayName("Product Name")]
        public string? NameProduct { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<CardProduct> CardProducts { get; set; }
    }
}
