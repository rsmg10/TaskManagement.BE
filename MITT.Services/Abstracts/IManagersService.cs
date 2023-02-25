using MITT.EmployeeDb.Models;

namespace MITT.Services.Abstracts;

public interface IManagerService
{
    Task<List<ProjectManagerVm>> Managers(ProjectTypeVm projectType, CancellationToken cancellationToken = default);

    Task<ManagersByProjectVm> Managers(string projectId, CancellationToken cancellationToken = default);

    Task<OperationResult> AddOrUpdateManager(ProjectManagerDto projectManagerDto, CancellationToken cancellationToken = default);

    Task<OperationResult> ChangeState(string managerId, CancellationToken cancellationToken = default);

    Task<OperationResult> AssignManagersToProject(AssignManagersDto assignManagersDto, CancellationToken cancellationToken = default);
}

public class ProjectManagerDto
{
    public string Id { get; set; }
    public string FullName { get; set; }
    public string NickName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}

public class ManagersByProjectVm
{
    public List<ProjectManagerVm> AssignedManagers { get; set; }
    public List<ProjectManagerVm> FreeManagers { get; set; }
}

public class ProjectManagerVm
{
    public string Id { get; set; }
    public string FullName { get; set; }
    public string NickName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public ActiveState ActiveState { get; set; }
    public int ActiveTasks { get; set; }
}

public class AssignManagersDto
{
    public string ProjectId { get; set; }
    public List<string> ManagerIds { get; set; }
}