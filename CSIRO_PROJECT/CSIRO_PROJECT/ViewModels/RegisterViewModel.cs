using System.ComponentModel.DataAnnotations;

namespace CSIRO_PROJECT.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }    

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password not matched")]
        public string ConfirmPassword { get; set; }
    }
}
