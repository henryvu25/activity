using System;
using System.ComponentModel.DataAnnotations;
namespace belt_project.Models
{
    public class CurrentDateAttribute : ValidationAttribute
    {
        public CurrentDateAttribute()
        {
        }

        public override bool IsValid(object value)
        {
            var dt = (DateTime)value;
            if (dt >= DateTime.Now)
            {
                return true;
            }
            return false;
        }
    }
    public class ActivityViewModel : BaseEntity
    {
        [Required(ErrorMessage = "Title is required.")]
        [MinLength(2, ErrorMessage = "Title must have at least 2 characters.")]
        public string Title { get; set; }

        [Required]
        [CurrentDate(ErrorMessage = "Please input future date")]
        public DateTime Date { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required(ErrorMessage = "Duration is required.")]
        public string num { get; set; }
        public string dur { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MinLength(10, ErrorMessage = "Description must have at least 10 characters.")]
        public string Description { get; set; }

        public int UserId { get; set; }
    }
}