using System.ComponentModel.DataAnnotations;

namespace CSIRO_PROJECT.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
