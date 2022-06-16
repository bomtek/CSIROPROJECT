using System.ComponentModel.DataAnnotations;

namespace CSIRO_PROJECT.ViewModels
{
    public class CourseModel
    {
        [Key]

        public int courseId { get; set; }

        [Display(Name = "Course Name")]
        [Required(ErrorMessage = "Course Name Required!")]
        [DataType(DataType.Text)]

        public string courseName { get; set; }
    }
}
