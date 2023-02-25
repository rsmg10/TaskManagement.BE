namespace MITT.EmployeeDb.Models
{
    public partial class QaReview : BaseEntity
    {
        public Guid AssignedQaTaskId { get; set; }
        public List<ReviewFinding> Findings { get; set; }

        public string FilePath { get; set; }

        public virtual AssignedQaTask AssignedQaTask { get; set; }
    }
}