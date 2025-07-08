using System.ComponentModel.DataAnnotations;

namespace ECommerceMini.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string Name { get; set; } = string.Empty;

        
        public ICollection<Product>? Products { get; set; }
    }
}
