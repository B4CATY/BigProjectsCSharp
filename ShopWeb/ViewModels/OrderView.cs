using System.ComponentModel;

namespace ShopWeb.ViewModels
{
    public class OrderView
    {
        [DisplayName("Order number")]
        public int CartId { get; set; }
        public string? Product { get; set; }
    }
}
