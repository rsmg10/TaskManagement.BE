namespace MITT.EmployeeDb.Models
{
    public partial class AssignedBeTask : BaseEntity
    {
        public AssignedBeTask()
        {
        }

        public static AssignedBeTask Create(DevTask devTask, Developer developer) => new()
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.Now,
            Developer = developer,
            DevTask = devTask,
            TaskState = TaskState.Pending
        };

        public TaskState TaskState { get; set; }
        public Guid DevTaskId { get; set; }
        public Guid DeveloperId { get; set; }

        public virtual DevTask DevTask { get; set; }
        public virtual Developer Developer { get; set; }
        public virtual ICollection<BeReview> BeReviews { get; set; } = new HashSet<BeReview>();
    }
}