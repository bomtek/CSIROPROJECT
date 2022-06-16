using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSIRO_PROJECT.ViewModels
{
    public class UniCourse
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("UniversityModel")]
        public int uniId { get; set; }
        public virtual UniversityModel uniIds { get; set; }

        [Required]
        [ForeignKey("CourseModel")]
        public int courseId { get; set; }
        public virtual CourseModel courseIds { get; set; }
    }
}

