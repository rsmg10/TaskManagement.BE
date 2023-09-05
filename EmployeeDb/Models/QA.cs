namespace MITT.EmployeeDb.Models;

public class QA : Employee
{
    public QA()
    {
        AssignedQaTasks = new HashSet<AssignedQaTask>();
    }

    public Guid ProjectManagerId { get; private set; }
    public Guid ProjectId { get; private set; }


    public virtual ICollection<AssignedQaTask> AssignedQaTasks { get; set; }
}