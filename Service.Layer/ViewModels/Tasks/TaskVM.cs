using System.ComponentModel.DataAnnotations;

namespace Service.Layer.ViewModels.Tasks
{
    public class TaskVM
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Task name is required.")]
        [StringLength(100, ErrorMessage = "Task name must be less than 100 characters.")]
        public string TaskName { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description must be less than 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Priority is required.")]
        public TaskPriority Priority { get; set; } = TaskPriority.None;

        [Required(ErrorMessage = "Due date is required.")]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        public string? AssignedUserId { get; set; }

        public string? AssignedUserName { get; set; } // For display purposes only
    }

    public enum TaskPriority
    {
        None = 0,
        Low = 1,
        Medium = 2,
        High = 3
    }
}
