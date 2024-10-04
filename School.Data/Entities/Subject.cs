using School.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace School.Data.Entities;
public class Subject : LocalizableEntity
{
    public Subject()
    {
        StudentsSubjects = new HashSet<StudentSubject>();
        DepartmetsSubjects = new HashSet<DepartmetSubject>();
    }
    [Key]
    public int SubID { get; set; }

    [StringLength(500)]
    public string SubjectNameEn { get; set; } = null!;

    [StringLength(500)]
    public string SubjectNameAr { get; set; } = null!;
    public DateTime Period { get; set; }
    public ICollection<StudentSubject> StudentsSubjects { get; set; }
    public ICollection<DepartmetSubject> DepartmetsSubjects { get; set; }
}
