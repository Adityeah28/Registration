using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Registration.Models
{
    public class Course
    {
         
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Course Name")]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1,100,ErrorMessage ="Display Order must be between 1-100")]
        public int DisplayOrder { get; set; }
    }
}
