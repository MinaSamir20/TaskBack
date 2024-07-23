using System.ComponentModel.DataAnnotations;

namespace TaskBack.Models.Entities
{
    public class Reminder
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Department Name is required.")]
        [StringLength(200, ErrorMessage = "Department Name can't be longer than 200 characters.")]
        public string? Title { get; set; }

        [Required]
        public DateTime ReminderDateTime { get; set; }

        public bool IsSent { get; set; } = false;
    }
}
