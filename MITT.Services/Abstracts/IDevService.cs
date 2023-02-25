using MITT.EmployeeDb.Models;

namespace MITT.Services.Abstracts;

public interface IDeveloperService
{
    Task<OperationResult> AddOrUpdateDeveloper(DeveloperDto developer, CancellationToken cancellationToken = default);

    Task<List<DeveloperVm>> Developers(bool activeOnly = true, CancellationToken cancellationToken = default);

    Task<List<DeveloperVm>> Developers(string taskId, CancellationToken cancellationToken = default);

    Task<OperationResult> ChangeState(string developerId, CancellationToken cancellationToken = default);
}

public class DeveloperDto
{
    public string Id { get; set; }
    public string FullName { get; set; }
    public string NickName { get; set; }
    public DeveloperType Type { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Pin { get; set; }
}

public class DeveloperVm
{
    public string Id { get; set; }
    public string FullName { get; set; }
    public string NickName { get; set; }
    public DeveloperType Type { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Pin { get; set; }
    public ActiveState ActiveState { get; set; }
    public List<DeveloperTaskVm> Tasks { get; set; } = new List<DeveloperTaskVm>();
}

public class DeveloperTaskVm
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> Requirements { get; set; }
    public string AssignedManagerName { get; set; }
    public string AssignedManagerId { get; set; }
    public ImplementationType ImplementationType { get; set; }
    public TaskState TaskState { get; set; } = TaskState.Pending;
    public double Progress { get; set; }
    public string CompletionMessage { get; set; }
    public DateTime? StartDate { get; set; } = null;
    public DateTime? EndDate { get; set; } = null;
}