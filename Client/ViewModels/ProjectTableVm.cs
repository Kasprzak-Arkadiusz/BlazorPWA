using Application.Common.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace Client.ViewModels
{
    public class ProjectTableVm
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(Constants.ProjectNameMaxLength, ErrorMessage = "Max length of {2} is {1} characters.")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Start date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Team id")]
        public string TeamId { get; set; }

        public string TechnologyNamesFlattened { get; set; }
        public string TechnologyToRemove { get; set; }
        public string TechnologyToAdd { get; set; }

        public string Technology { get; set; }
    }
}