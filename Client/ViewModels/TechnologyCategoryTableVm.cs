using System.ComponentModel.DataAnnotations;

namespace Client.ViewModels
{
    public class TechnologyCategoryTableVm
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(40, ErrorMessage = "Max length of {2} is {1} characters.")]
        public string Name { get; set; }
    }
}
