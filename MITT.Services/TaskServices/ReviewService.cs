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
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(addBeReview.AssignedTaskId), cancellationToken) ?? throw new Exception();

        var backEndReview = BeReview.Create(assignedBeTask, addBeReview.Findings);

        assignedBeTask.BeReviews.Add(backEndReview);

        await _managementDb.SaveChangesAsync(cancellationToken);
        return OperationResult.Valid();
    }

    public async Task<OperationResult> AddQaReview(AddReviewDto addQaReview, CancellationToken cancellationToken)
    {
        var assignedQaTask = await _managementDb.AssignedQatasks
            .Include(x => x.Developer)
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(addQaReview.AssignedTaskId), cancellationToken) ?? throw new Exception();

        var qaReview = QaReview.Create(assignedQaTask, addQaReview.Findings);

        assignedQaTask.QaReviews.Add(qaReview);

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
}