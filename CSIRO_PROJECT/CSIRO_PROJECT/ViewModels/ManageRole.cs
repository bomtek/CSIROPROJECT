using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CSIRO_PROJECT.ViewModels
{
    public class ManageRole
    {
        [Display(Name ="User ID")]
        public string userId { get; set; }


        [Display(Name="Role Id")]
        public string roleId { get; set; }


        [Display(Name = "User List")]
        public List<SelectListItem> userList { get; set; }



        [Display(Name = "Role List")]
        public List<SelectListItem> roleList { get; set; }
    }


}
