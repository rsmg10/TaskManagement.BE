namespace MITT.EmployeeDb.Models;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; } = null;
}

public abstract class Identity : BaseEntity
{
    public string First { get; set; }
    public string Last { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Pin { get; set; }
    public bool IsSigned { get; set; } = false;

    public string FullName() => $"{First} {Last}";
}

public enum ActiveState
{
    Inactive, Active
}