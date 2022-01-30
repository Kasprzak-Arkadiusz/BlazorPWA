using System.ComponentModel.DataAnnotations;

namespace Client.ViewModels
{
    public class TechnologyTableVm
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "Max length of {2} is {1} characters.")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string CategoryName { get; set; }
    }
}