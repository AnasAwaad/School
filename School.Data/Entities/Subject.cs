using School.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Data.Entities;
public class Subject : LocalizableEntity
{
    public Subject()
    {
        StudentsSubjects = new HashSet<StudentSubject>();
        DepartmetsSubjects = new HashSet<DepartmetSubject>();
        InstructorSubjects = new HashSet<InstructorSubject>();
    }
    [Key]
    public int SubID { get; set; }

    [StringLength(500)]
    public string? SubjectNameEn { get; set; }

    [StringLength(500)]
    public string? SubjectNameAr { get; set; }
    public int? Period { get; set; }
    public ICollection<StudentSubject> StudentsSubjects { get; set; }
    public ICollection<DepartmetSubject> DepartmetsSubjects { get; set; }
    [InverseProperty("Subject")]
    public ICollection<InstructorSubject> InstructorSubjects { get; set; }
}
