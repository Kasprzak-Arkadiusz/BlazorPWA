using Application.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace Client.ViewModels
{
    public class EmployeeTableVm
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "First name")]
        [StringLength(Constants.EmployeeFirstNameMaxLength, ErrorMessage = "Max length of {2} is {1} characters.")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        [StringLength(Constants.EmployeeLastNameMaxLength, ErrorMessage = "Max length of {2} is {1} characters.")]
        public string LastName { get; set; }

        [Required]
        [Range(Constants.EmployeeMinAge, Constants.EmployeeMaxAge,
            ErrorMessage = "Employee must not be younger than 18 or more than 100 years old.")]
        public int Age { get; set; }

        public string TechnologyNamesFlattened { get; set; }
        public string TechnologyToRemove { get; set; }
        public string TechnologyToAdd { get; set; }

        public string Technology { get; set; }

        [Required]
        [Display(Name = "Team id")]
        public string TeamId { get; set; }
    }
}