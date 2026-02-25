using Mission8.Models;
using System.ComponentModel.DataAnnotations;

namespace Mission8.Models
{
    public class TaskItem
    {
        [Key]
        public int TaskId { get; set; }
        [Required]
        public string TaskName { get; set; }
        public DateTime? DueDate { get; set; }

        [Required]
        public int Quadrant { get; set; }
        public bool Completed { get; set; } = false;

        //Foreign Key
        [Required]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
