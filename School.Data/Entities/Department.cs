using School.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Data.Entities;
public partial class Department : LocalizableEntity
{
    public Department()
    {
        Students = new HashSet<Student>();
        DepartmentSubjects = new HashSet<DepartmetSubject>();
        Instructors = new HashSet<Instructor>();
    }
    [Key]
    public int DID { get; set; }
    [StringLength(500)]
    public string DNameEn { get; set; } = null!;
    [StringLength(500)]
    public string DNameAr { get; set; } = null!;



    [InverseProperty("DepartmentManage")]
    public Instructor InstructorManage { get; set; }


    public ICollection<Student> Students { get; set; }
    public ICollection<DepartmetSubject> DepartmentSubjects { get; set; }

    [InverseProperty("Department")]
    public ICollection<Instructor> Instructors { get; set; }
}