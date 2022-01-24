using System.ComponentModel.DataAnnotations;

namespace ShopWeb.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Login not specified")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password not specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password is incorrect")]
        public string ConfirmPassword { get; set; }
    }
}