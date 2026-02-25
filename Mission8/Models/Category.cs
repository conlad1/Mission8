using System.ComponentModel.DataAnnotations;

namespace Mission8.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public List<TaskItem> Tasks { get; set; }
    }
}

