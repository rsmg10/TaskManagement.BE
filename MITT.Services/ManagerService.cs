using Microsoft.EntityFrameworkCore;
using MITT.EmployeeDb;
using MITT.EmployeeDb.Models;
using MITT.Services.Abstracts;
using MITT.Services.Helpers;

namespace MITT.Services;

public class ManagerService : ManagementService<Manager>, IManagerService
{
    private readonly ManagementDb _managementDb;

    public ManagerService(ManagementDb managementDb) : base(managementDb) => _managementDb = managementDb;

    public async Task<List<ProjectManagerVm>> Managers(ProjectTypeVm projectType, CancellationToken cancellationToken = default)
    {
        var list = new List<ProjectManagerVm>();

        var type = Parse(projectType);

        var managers = projectType == ProjectTypeVm.All ?
            await _managementDb.Managers.ToListAsync(cancellationToken) :
            await _managementDb.Managers
            .Include(x => x.AssignedManagers)
            .ThenInclude(x => x.Project)
            .Where(x => x.AssignedManagers.Any(x => x.Project.ProjectType == type))
            .ToListAsync(cancellationToken);

        foreach (var manager in managers) list.Add(new ProjectManagerVm
        {
            Id = manager.Id.ToString(),
            FullName = manager.FullName(),
            NickName = manager.First,
            Phone = manager.Phone,
            Email = manager.Email,
            ActiveState = manager.ActiveState,
            ActiveTasks = await GetAssignedTasks(manager, cancellationToken)
        });

        return list;
    }

    public async Task<ManagersByProjectVm> Managers(string projectId, CancellationToken cancellationToken = default)
    {
        var assignedManagers = new List<ProjectManagerVm>();
        var freeManagers = new List<ProjectManagerVm>();

        var assignedManagersData = await _managementDb.AssignedManagers
            .Include(x => x.ProjectManager)
            .Where(x => x.ProjectId == Guid.Parse(projectId))
            .ToListAsync(cancellationToken);

        foreach (var manager in assignedManagersData) assignedManagers.Add(new ProjectManagerVm
        {
            Id = manager.Id.ToString(),
            FullName = manager.ProjectManager.FullName(),
            NickName = manager.ProjectManager.First,
            Phone = manager.ProjectManager.Phone,
            Email = manager.ProjectManager.Email,
            ActiveState = manager.ProjectManager.ActiveState,
            ActiveTasks = await GetAssignedTasks(manager.ProjectManager, cancellationToken)
        });

        var freeManagersData = await _managementDb.Managers
            .Where(x => x.ActiveState == ActiveState.Active)
            .ToListAsync(cancellationToken);

        foreach (var manager in freeManagersData)
        {
            if (!assignedManagers.Select(x => x.FullName).ToList().Contains(manager.FullName())) freeManagers.Add(new ProjectManagerVm
            {
                Id = manager.Id.ToString(),
                FullName = manager.FullName(),
                NickName = manager.First,
                Phone = manager.Phone,
                Email = manager.Email,
                ActiveState = manager.ActiveState
            });
        }

        return new ManagersByProjectVm
        {
            AssignedManagers = assignedManagers,
            FreeManagers = freeManagers
        };
    }

    public async Task<OperationResult> AddOrUpdateManager(ProjectManagerDto projectManagerDto, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(projectManagerDto.Id))
        {
            var entity = Manager.Create(projectManagerDto.FullName, projectManagerDto.NickName, projectManagerDto.Email, projectManagerDto.Phone);

            await Add(entity, cancellationToken);
            return OperationResult.Valid();
        }

        var manager = await _managementDb.Managers.FirstOrDefaultAsync(x => x.Id == Guid.Parse(projectManagerDto.Id), cancellationToken);

        if (manager is null) throw new Exception($"invalid_{manager}_id!!");

        manager.Update(projectManagerDto?.FullName, projectManagerDto?.NickName, projectManagerDto?.Phone, projectManagerDto?.Email);

        await Update(manager, cancellationToken);
        return OperationResult.Valid();
    }

    public async Task<OperationResult> AssignManagersToProject(AssignManagersDto assignManagersDto, CancellationToken cancellationToken = default)
    {
        var managers = new List<Manager>();
        var managerIdList = assignManagersDto.ManagerIds.Select(Guid.Parse);

        var project = await _managementDb.Projects
            .Include(x => x.AssignedManagers)
            .ThenInclude(x => x.ProjectManager)
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(assignManagersDto.ProjectId), cancellationToken);

        if (project is null) throw new Exception($"{nameof(project)} is null!!");

        await ValidateManagerList(managers, managerIdList, project, cancellationToken);

        foreach (var manager in managers) project.AssignedManagers.Add(AssignedManager.Create(project, manager));

        await Save(cancellationToken);
        return OperationResult.Valid();
    }

    public async Task<OperationResult> ChangeState(string managerId, CancellationToken cancellationToken = default)
    {
        var developer = await _managementDb.Managers.FirstOrDefaultAsync(x => x.Id == Guid.Parse(managerId), cancellationToken);

        if (developer == null) throw new Exception("invalid_develoepr_id_provided!!");

        if (developer.ActiveState == ActiveState.Active) developer.ActiveState = ActiveState.Inactive;
        else developer.ActiveState = ActiveState.Active;

        await Update(developer, cancellationToken);
        return OperationResult.Valid();
    }

    private async Task ValidateManagerList(List<Manager> managers, IEnumerable<Guid> managerIdList, Project project, CancellationToken cancellationToken)
    {
        foreach (var managerId in managerIdList)
        {
            var manager = await Get(managerId, cancellationToken);
            if (manager is null) continue;

            if (!project.AssignedManagers.Any(assignedManager => managerIdList.Contains(assignedManager.ProjectManagerId))) managers.Add(manager);
        }
    }

    private async Task<int> GetAssignedTasks(Manager manager, CancellationToken cancellationToken)
    {
        var assignedProjectCount = 0;
        var assignedProjects = await _managementDb.AssignedManagers
            .Where(x => x.ProjectManagerId == manager.Id)
            .ToListAsync(cancellationToken);

        foreach (var projectManager in assignedProjects)
        {
            var count = await _managementDb.Tasks
                .Where(devTask => devTask.AssignedManagerId == projectManager.Id && devTask.TaskState == TaskState.Pending)
                .CountAsync(cancellationToken);

            assignedProjectCount += count;
        }

        return assignedProjectCount;
    }

    private ProjectType? Parse(ProjectTypeVm projectTypeVm) => projectTypeVm switch
    {
        ProjectTypeVm.Mb => ProjectType.Mb,
        ProjectTypeVm.Py => ProjectType.Py,
        ProjectTypeVm.Wb => ProjectType.Wb,
        ProjectTypeVm.Ot => ProjectType.Ot,
        ProjectTypeVm.All => null,
        _ => null,
    };
}

public enum ProjectTypeVm
{
    Mb = 1, Py, Wb, Ot, All
}