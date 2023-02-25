using Microsoft.EntityFrameworkCore;
using MITT.EmployeeDb;
using MITT.EmployeeDb.Models;
using MITT.Services.Abstracts;

namespace MITT.Services.TaskServices;

public class ReviewService
{
    private readonly ManagementDb _managementDb;

    public ReviewService(ManagementDb managementDb) => _managementDb = managementDb;

    public async Task<OperationResult> AddBeReview(AddReviewDto addBeReview, CancellationToken cancellationToken)
    {
        var assignedBeTask = await _managementDb.AssignedBetasks
            .Include(x => x.Developer)
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(addBeReview.AssignedTaskId), cancellationToken);

        if (assignedBeTask is null) throw new Exception();

        assignedBeTask.BeReviews.Add(new BeReview
        {
            AssignedBeTask = assignedBeTask,
            Findings = addBeReview.Findings,
            CreatedAt = addBeReview.CreatedAt
        });

        await _managementDb.SaveChangesAsync(cancellationToken);
        return OperationResult.Valid();
    }

    public async Task<OperationResult> AddQaReview(AddReviewDto addQaReview, CancellationToken cancellationToken)
    {
        var assignedQaTask = await _managementDb.AssignedQatasks
            .Include(x => x.Developer)
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(addQaReview.AssignedTaskId), cancellationToken);

        if (assignedQaTask is null) throw new Exception();

        assignedQaTask.QaReviews.Add(new QaReview
        {
            AssignedQaTask = assignedQaTask,
            Findings = addQaReview.Findings,
            CreatedAt = addQaReview.CreatedAt
        });

        await _managementDb.SaveChangesAsync(cancellationToken);
        return OperationResult.Valid();
    }
}

public class AddReviewDto
{
    public AssignDevType AssignDevType { get; set; }
    public string AssignedTaskId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public List<ReviewFinding> Findings { get; set; }
    //public FileInfo File { get; set; }
}