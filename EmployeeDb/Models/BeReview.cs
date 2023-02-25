namespace MITT.EmployeeDb.Models
{
    public partial class BeReview : BaseEntity
    {
        public BeReview()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

        public Guid AssignedBeTaskId { get; set; }
        public List<ReviewFinding> Findings { get; set; }
        public string FilePath { get; set; }

        public virtual AssignedBeTask AssignedBeTask { get; set; }
    }

    public class ReviewFinding
    {
        public string Location { get; set; }
        public string Scope { get; set; }
        public string Discription { get; set; }
    }
}