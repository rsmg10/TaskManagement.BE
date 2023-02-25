namespace MITT.EmployeeDb.Models
{
    public partial class Developer : Identity
    {
        public Developer()
        {
            AssignedBetasks = new HashSet<AssignedBeTask>();
            AssignedQatasks = new HashSet<AssignedQaTask>();
        }

        public DeveloperType Type { get; set; }
        public string Image { get; set; }
        public ActiveState ActiveState { get; set; } = ActiveState.Active;
        public virtual ICollection<AssignedBeTask> AssignedBetasks { get; set; }
        public virtual ICollection<AssignedQaTask> AssignedQatasks { get; set; }

        public static Developer Create(string firstName, string lastName, string email, string phone, string pin, DeveloperType developerType) => new()
        {
            Id = Guid.NewGuid(),
            First = firstName,
            Last = lastName,
            Email = email,
            Phone = phone,
            Pin = pin,
            Type = developerType,
            CreatedAt = DateTime.Now
        };

        public void Update(string firstName, string lastName, string email, string phone, DeveloperType developerType)
        {
            First = firstName;
            Last = lastName;
            Phone = email;
            Email = email;
            Type = developerType;
            UpdatedAt = DateTime.Now;
        }
    }

    public enum DeveloperType
    {
        Be = 1, Qa, Pm, Rv
    }
}