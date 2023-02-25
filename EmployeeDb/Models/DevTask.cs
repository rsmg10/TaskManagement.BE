namespace MITT.EmployeeDb.Models
{
    public partial class DevTask : BaseEntity
    {
        public DevTask()
        {
            AssignedBetasks = new HashSet<AssignedBeTask>();
            AssignedQatasks = new HashSet<AssignedQaTask>();
        }

        public string SeqNo { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public List<string> Requirements { get; private set; }
        public ImplementationType ImplementationType { get; private set; }
        public TaskState TaskState { get; set; }
        public string CompletionMessage { get; set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public Guid? AssignedManagerId { get; private set; }

        public virtual AssignedManager AssignedManager { get; set; }
        public virtual ICollection<AssignedBeTask> AssignedBetasks { get; private set; }
        public virtual ICollection<AssignedQaTask> AssignedQatasks { get; private set; }

        public static DevTask Create(string seqNo,
                                     string name,
                                     string description,
                                     DateTime startDate,
                                     DateTime endDate,
                                     ImplementationType implType,
                                     List<string> req,
                                     Guid assignedManagerId) => new()
                                     {
                                         Id = Guid.NewGuid(),
                                         CreatedAt = DateTime.Now,
                                         SeqNo = seqNo,
                                         Name = name,
                                         Description = description,
                                         StartDate = startDate,
                                         EndDate = endDate,
                                         ImplementationType = implType,
                                         Requirements = req,
                                         AssignedManagerId = assignedManagerId,
                                         TaskState = TaskState.Pending,
                                     };

        public void AddQaDevelopers(IEnumerable<Developer> developers)
        {
            foreach (Developer developer in developers)
            {
                AssignedQatasks.Add(AssignedQaTask.Create(this, developer));
            }
        }

        public void AddBeDevelopers(IEnumerable<Developer> developers)
        {
            foreach (Developer developer in developers)
            {
                AssignedBetasks.Add(AssignedBeTask.Create(this, developer));
            }
        }

        public void Update(string name, string description, DateTime startDate, DateTime endDate, ImplementationType implType, List<string> req)
        {
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            ImplementationType = implType;
            Requirements = req;
        }
    }

    public enum TaskState
    {
        Pending = 1, Completed, Canceled
    }

    public enum ImplementationType
    {
        Implementation = 1, Refactoring
    }
}