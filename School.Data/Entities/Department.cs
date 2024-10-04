using School.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace School.Data.Entities;
public partial class Department : LocalizableEntity
{
    public Department()
    {
        Students = new HashSet<Student>();
        DepartmentSubjects = new HashSet<DepartmetSubject>();
    }
    [Key]
    public int DID { get; set; }
    [StringLength(500)]
    public string DNameEn { get; set; } = null!;
    [StringLength(500)]
    public string DNameAr { get; set; } = null!;
    public ICollection<Student> Students { get; set; }
    public ICollection<DepartmetSubject> DepartmentSubjects { get; set; }
}