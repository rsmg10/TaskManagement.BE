using Microsoft.EntityFrameworkCore;
using MITT.EmployeeDb;
using MITT.EmployeeDb.Models;
using MITT.Services.Abstracts;
using MITT.Services.Helpers;

namespace MITT.Services.TaskServices;

public class TaskService : ManagementService<DevTask>, ITaskService
{
    private readonly ManagementDb _managementDb;

    public TaskService(ManagementDb managementDb) : base(managementDb) => _managementDb = managementDb;

    public async Task<List<TaskVm>> Tasks(string projectId, string developerId, bool activeOnly = true, CancellationToken cancellationToken = default)
    {
        var list = new List<TaskVm>();

        List<DevTask> tasks = await GetTasks(projectId, developerId, cancellationToken);

        foreach (var task in tasks) list.Add(new TaskVm
        {
            Id = task.Id.ToString(),
            SeqNo = task.SeqNo,
            AssignedManagerId = task.AssignedManagerId.ToString(),
            AssignedManagerName = task.AssignedManager.ProjectManager.FullName(),
            CompletionMessage = task.CompletionMessage ?? string.Empty,
            Description = task.Description,
            ImplementationType = task.ImplementationType,
            EndDate = task.EndDate,
            StartDate = task.StartDate,
            Name = task.Name,
            Requirements = task.Requirements,
            TaskState = task.TaskState,
            AssignedProjectId = task.AssignedManager.ProjectId.ToString(),
            AssignedProjectName = task.AssignedManager.Project.Name,
            TimeLeft = string.Empty,
            Progress = string.Empty,
            TimeAllow = 0,
            AssignedBeDevs = await GetAssignedBeDevs(task),
            AssignedQaDevs = await GetAssignedQaDevs(task)
        });

        return list.OrderBy(x => x.SeqNo)
            .ThenBy(x => x.AssignedBeDevs)
            .ToList();
    }

    public async Task<List<TaskNamesVm>> TaskNames(CancellationToken cancellationToken = default) => await _managementDb.Tasks
        .Include(x => x.AssignedManager)
        .ThenInclude(x => x.Project)
        .Where(x => x.TaskState == TaskState.Pending)
        .Select(x => new TaskNamesVm
        {
            Id = x.Id.ToString(),
            SeqNo = x.SeqNo,
            Name = x.Name,
            StartDate = x.StartDate,
            EndDate = x.EndDate,
            ProjectType = x.AssignedManager.Project.ProjectType,
            ImplementationType = x.ImplementationType
        }).ToListAsync(cancellationToken);

    public async Task<OperationResult> AddOrUpdateTask(TaskDto taskDto, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(taskDto.Id))
        {
            var manager = await _managementDb.AssignedManagers
                .Include(x => x.Project)
                .FirstOrDefaultAsync(x => x.Id == Guid.Parse(taskDto.AssignedManagerId), cancellationToken);

            var seqNo = await _managementDb.GenerateSequance(manager.Project.ProjectType);

            var entity = DevTask.Create(seqNo,
                                        taskDto.Name,
                                        taskDto.Description,
                                        taskDto.StartDate,
                                        taskDto.EndDate,
                                        taskDto.ImplementationType,
                                        taskDto.Requirements,
                                        Guid.Parse(taskDto.AssignedManagerId));

            await Add(entity, cancellationToken);

            return OperationResult.Valid();
        }

        var task = await Get(Guid.Parse(taskDto.Id), cancellationToken);

        if (task is null) throw new Exception($"invalid_{nameof(task)}_id!!");

        task.Update(taskDto.Name, taskDto.Description, taskDto.StartDate, taskDto.EndDate, taskDto.ImplementationType, taskDto.Requirements);

        await Update(task, cancellationToken);

        return OperationResult.Valid();
    }

    public async Task<OperationResult> CompleteTaskByReviewer(string reviewerId, string taskId, string message, CancellationToken cancellationToken = default)
    {
        var reviewer = await _managementDb.Developers.FirstOrDefaultAsync(x => x.Id == Guid.Parse(reviewerId) && x.Type == DeveloperType.Rv, cancellationToken);

        if (reviewer is null) throw new Exception();

        var task = await _managementDb.Tasks.FirstOrDefaultAsync(x => x.Id == Guid.Parse(taskId) && x.TaskState == TaskState.Pending, cancellationToken);

        if (task is null) throw new Exception();

        if (!string.IsNullOrEmpty(message))
        {
            task.TaskState = TaskState.Canceled;
            task.CompletionMessage = message;
        }
        else task.TaskState = TaskState.Completed;

        await Update(task, cancellationToken);

        return OperationResult.Valid();
    }

    #region helpers

    private async Task<List<DevTask>> GetTasks(string projectId, string developerId, CancellationToken cancellationToken)
    {
        if (Guid.TryParse(projectId, out var project))
        {
            return await _managementDb.Tasks
                .Include(x => x.AssignedManager)
                .ThenInclude(x => x.ProjectManager)
                .Include(x => x.AssignedManager)
                .ThenInclude(x => x.Project)
                .Where(x => x.AssignedManager.ProjectId == project)
                .ToListAsync(cancellationToken);
        }

        if (Guid.TryParse(developerId, out var developer))
        {
            var dev = await _managementDb.Developers.FirstOrDefaultAsync(x => x.Id == developer, cancellationToken);

            return dev.Type == DeveloperType.Be ?
             await _managementDb.Tasks
                .Include(x => x.AssignedManager)
                .ThenInclude(x => x.ProjectManager)
                .Include(x => x.AssignedManager)
                .ThenInclude(x => x.Project)
                .Where(devTask => devTask.AssignedBetasks.Any(assignedDevTask => assignedDevTask.DeveloperId == developer) && devTask.TaskState == TaskState.Pending)
                .ToListAsync(cancellationToken) :
                 await _managementDb.Tasks
                .Include(x => x.AssignedManager)
                .ThenInclude(x => x.ProjectManager)
                .Include(x => x.AssignedManager)
                .ThenInclude(x => x.Project)
                .Where(devTask => devTask.AssignedQatasks.Any(assignedDevTask => assignedDevTask.DeveloperId == developer) && devTask.TaskState == TaskState.Pending)
                .ToListAsync(cancellationToken);
        }

        return await _managementDb.Tasks
            .Include(x => x.AssignedManager)
            .ThenInclude(x => x.ProjectManager)
            .Include(x => x.AssignedManager)
            .ThenInclude(x => x.Project)
            .Where(x => x.TaskState == TaskState.Pending)
            .ToListAsync(cancellationToken);
    }

    private async Task<List<AssignedDevVm>> GetAssignedBeDevs(DevTask devTask)
    {
        var list = new List<AssignedDevVm>();

        var tasks = await _managementDb.AssignedBetasks
            .Where(x => x.DevTaskId == devTask.Id)
            .Include(x => x.Developer)
            .Include(x => x.BeReviews)
            .ToListAsync();

        foreach (var task in tasks) list.Add(new AssignedDevVm
        {
            AssignedTaskId = task.Id.ToString(),
            DevId = task.DeveloperId.ToString(),
            Name = task.Developer.FullName(),
            Email = task.Developer.Email,
            Phone = task.Developer.Phone,
            Reviews = task.BeReviews.Select(x => new ReviewVm
            {
                ReviewDate = x.CreatedAt,
                Findings = x.Findings
            }).ToList()
        });

        return list;
    }

    private async Task<List<AssignedDevVm>> GetAssignedQaDevs(DevTask devTask)
    {
        var list = new List<AssignedDevVm>();

        var tasks = await _managementDb.AssignedQatasks
            .Where(x => x.DevTaskId == devTask.Id)
            .Include(x => x.Developer)
            .Include(x => x.QaReviews)
            .ToListAsync();

        foreach (var task in tasks) list.Add(new AssignedDevVm
        {
            AssignedTaskId = task.Id.ToString(),
            DevId = task.DeveloperId.ToString(),
            Name = task.Developer.FullName(),
            Email = task.Developer.Email,
            Phone = task.Developer.Phone,
            Reviews = task.QaReviews.Select(x => new ReviewVm
            {
                ReviewDate = x.CreatedAt,
                Findings = x.Findings
            }).ToList()
        });

        return list;
    }

    #endregion helpers
}