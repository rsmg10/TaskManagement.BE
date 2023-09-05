using Microsoft.EntityFrameworkCore;

namespace MITT.EmployeeDb.Models;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; } = null;
}

public abstract class Employee : BaseEntity
{
    public ActiveState ActiveState { get; set; } = ActiveState.Active;

    public string FullName { get; set; }
    public string NickName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Pin { get; set; }
    
    // public HierarchyId HierarchyId { get; set; }
    
    public EmployeeType EmployeeType { get; set; }
    public bool IsSigned { get; set; } = false;
    public bool IsReviewer { get; set; } = false;
}

public enum ActiveState
{
    Inactive, Active
}

public enum EmployeeType
{
    Developer, PM, QA
}