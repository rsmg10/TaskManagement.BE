using Microsoft.EntityFrameworkCore;
using MITT.EmployeeDb;
using MITT.EmployeeDb.Models;
using MITT.Services.Abstracts;

namespace MITT.Services.TaskServices;

public class AssignmentService
{
    private readonly ManagementDb _managementDb;

    public AssignmentService(ManagementDb managementDb) => _managementDb = managementDb;

    public async Task<OperationResult> AssignBETask(AssignTaskDto assignTaskDto, CancellationToken cancellationToken = default)
    {
        var developers = new List<Developer>();

        var task = await _managementDb.Tasks
            .Include(x => x.AssignedBetasks)
            .ThenInclude(x => x.Developer)
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(assignTaskDto.TaskId), cancellationToken);

        if (task is null) throw new Exception($"{nameof(task)} is null");

        IEnumerable<Guid> developerIds = assignTaskDto.DeveloperIds.Select(Guid.Parse);

        foreach (var developerId in developerIds)
        {
            var developer = await _managementDb.Developers.FirstOrDefaultAsync(x => x.Id == developerId, cancellationToken);
            if (developer is null) continue;

            if (!task.AssignedBetasks.Any(x => developerIds.Contains(x.DeveloperId))) developers.Add(developer);
        }

        task.AddBeDevelopers(developers);

        await _managementDb.SaveChangesAsync(cancellationToken);
        return OperationResult.Valid();
    }

    public async Task<OperationResult> AssignQATask(AssignTaskDto assignTaskDto, CancellationToken cancellationToken = default)
    {
        var developers = new List<Developer>();

        var task = await _managementDb.Tasks
            .Include(x => x.AssignedQatasks)
            .ThenInclude(x => x.Developer)
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(assignTaskDto.TaskId), cancellationToken);

        if (task is null) throw new Exception($"{nameof(task)} is null");

        IEnumerable<Guid> develoersId = assignTaskDto.DeveloperIds.Select(Guid.Parse);

        foreach (var developerId in develoersId)
        {
            var developer = await _managementDb.Developers.FirstOrDefaultAsync(x => x.Id == developerId, cancellationToken);
            if (developer is null) continue;

            if (!task.AssignedQatasks.Any(x => develoersId.Contains(x.DeveloperId))) developers.Add(developer);
        }

        task.AddQaDevelopers(developers);

        await _managementDb.SaveChangesAsync(cancellationToken);
        return OperationResult.Valid();
    }
}