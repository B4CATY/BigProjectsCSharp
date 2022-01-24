using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShopWeb.ViewModels
{
    public class EditPassword
    {
        [DisplayName("New Password")]
        [Required(ErrorMessage = "Password not specified")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        public string OldPassword { get; set; }


        [DisplayName("Old Password")]
        [Required(ErrorMessage = "Old password not specified")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
