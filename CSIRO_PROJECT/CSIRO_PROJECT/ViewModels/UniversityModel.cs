


using System.ComponentModel.DataAnnotations;

namespace CSIRO_PROJECT.ViewModels
{
    public class UniversityModel
    { 
       [Key]
        public int uniId { get; set; }
        public string uniName { get; set; }

        
    }
}
