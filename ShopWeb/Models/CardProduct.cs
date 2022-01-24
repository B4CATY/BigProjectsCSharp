using System;
using System.Collections.Generic;

namespace ShopWeb.Models
{
    public partial class CardProduct
    {
        public int Id { get; set; }
        public int? Productid { get; set; }
        public int? Cardid { get; set; }

        public virtual Cart? Card { get; set; }
        public virtual Product? Product { get; set; }
    }
}
