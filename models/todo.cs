using System.ComponentModel.DataAnnotations;

namespace todoapi.Models
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Status { get; set; } = "Pending"; // "Pending" or "Completed"
    }
}
