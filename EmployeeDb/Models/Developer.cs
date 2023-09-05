using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MITT.EmployeeDb.Models
{
    public partial class Developer : Employee
    {
        public Developer()
        {
            AssignedBetasks = new HashSet<AssignedBeTask>();
            // AssignedQatasks = new HashSet<AssignedQaTask>();
        }
        // public hierarchyid  Hierarchyid { get; set; }
        public DeveloperType DevType { get; set; }
        
        public string Image { get; set; }
        public virtual ICollection<AssignedBeTask> AssignedBetasks { get; set; }

        public static Developer Create(string fullName, string nickName, string email, string phone, string pin, DeveloperType developerType) => new()
        {
            Id = Guid.NewGuid(),
            FullName = fullName,
            NickName = nickName,
            Email = email,
            Phone = phone,
            Pin = pin,
            DevType = developerType,
            HierarchyId = HierarchyId.Parse("/") ,
            CreatedAt = DateTime.Now
        };

        public void Update(string fullName, string nickName, string email, string phone, DeveloperType developerType)
        {
            FullName = fullName;
            NickName = nickName;
            Phone = email;
            Email = email;
            DevType = developerType;
            UpdatedAt = DateTime.Now;
        }
    }

    public enum DeveloperType
    {
        Be = 1, Qa, Pm, Rv
    }
}