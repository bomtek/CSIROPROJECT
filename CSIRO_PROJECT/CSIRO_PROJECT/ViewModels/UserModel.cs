using System.ComponentModel.DataAnnotations;

namespace CSIRO_PROJECT.ViewModels
{
    public class UserModel
    {


            [Key]
            public int userId { get; set; }

            [Display(Name = "First Name")]
            [Required(ErrorMessage = "First name required")]
            public string userFName { get; set; }

            [Display(Name = "Last Name")]
            [Required(ErrorMessage = "Last Name Required")]
            public string userLName { get; set; }

            [Display(Name = "Address")]
            [Required(ErrorMessage = "Address Required")]
            public string userAddress { get; set; }

            [Display(Name = "University")]
            [Required(ErrorMessage = "University Field Required")]
            public string userUni { get; set; }

            [Display(Name = "Course")]

            public string userCourse { get; set; }

            [Display(Name ="GPA")]
            [Required(ErrorMessage ="GPA required")]
            public double userGPA { get; set; }

            [Display(Name = "Phone")]
            [Required(ErrorMessage = "Phone Number Required")]
            public string userContact { get; set; }


            [Display(Name = "Coverletter")]
            [DataType(DataType.MultilineText)]
            public string userCL { get; set; }


        }
    }

