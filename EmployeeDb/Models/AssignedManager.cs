namespace MITT.EmployeeDb.Models
{
    public partial class AssignedManager : BaseEntity
    {
        public AssignedManager()
        {
        }

        public static AssignedManager Create(Project project, Manager manager) => new()
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.Now,
            Project = project,
            ProjectManager = manager,
        };

        public Guid ProjectManagerId { get; private set; }
        public Guid ProjectId { get; private set; }

        public virtual Project Project { get; set; }
        public virtual Manager ProjectManager { get; set; }
        public virtual ICollection<DevTask> Tasks { get; private set; } = new HashSet<DevTask>();
    }
}