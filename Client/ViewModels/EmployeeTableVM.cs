using System.ComponentModel.DataAnnotations;

namespace Client.ViewModels
{
    public class EmployeeTableVM
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "First name")]
        [StringLength(40, ErrorMessage = "Max length of first name is {1} characters.")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last name")]
        [StringLength(40, ErrorMessage = "Max length of last name is {1} characters.")]
        public string LastName { get; set; }
        [Required]
        [Range(18, 100, ErrorMessage = "Employee must not be younger than 18 or more than 100 years old.")]
        public int Age { get; set; }
        public string TechnologyNamesFlattened { get; set; }
        public string TechnologyToRemove { get; set; }
        public string TechnologyToAdd { get; set; }
        [Required]
        public string Technology { get; set; }
        [Required]
        [Display(Name = "Team id")]
        public string TeamId { get; set; }
    }
}