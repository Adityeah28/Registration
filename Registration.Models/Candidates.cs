using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.Models
{
    public class Candidates
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Class { get; set; }

        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        [ValidateNever]
        public Course Course { get; set; }
        [ValidateNever]
        public string ImageUrl {  get; set; }
        [ValidateNever]
        public string? Status { get; set; }="Pending";
        

    }
}
