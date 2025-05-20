using System.ComponentModel.DataAnnotations;

namespace TaskManagerProLite.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
