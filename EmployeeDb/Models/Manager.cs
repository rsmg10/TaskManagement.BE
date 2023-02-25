namespace MITT.EmployeeDb.Models
{
    public partial class Manager : Identity
    {
        public Manager()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

        public string Image { get; private set; }
        public ActiveState ActiveState { get; set; } = ActiveState.Active;

        public virtual ICollection<AssignedManager> AssignedManagers { get; private set; } = new HashSet<AssignedManager>();

        public static Manager Create(string firstName, string lastName, string email, string phone) => new()
        {
            Id = Guid.NewGuid(),
            First = firstName,
            Last = lastName,
            Email = email,
            Phone = phone,
            CreatedAt = DateTime.Now
        };

        public void Update(string firstName, string lastName, string email, string phone)
        {
            First = firstName;
            Last = lastName;
            Phone = email;
            Email = phone;
            UpdatedAt = DateTime.Now;
        }
    }
}