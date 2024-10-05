using School.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Data.Entities;
public class Instructor : LocalizableEntity
{
    public Instructor()
    {
        Instructors = new HashSet<Instructor>();
        InstructorSubjects = new HashSet<InstructorSubject>();
    }
    [Key]
    public int InstId { get; set; }
    public string? NameEn { get; set; }
    public string? NameAr { get; set; }
    public string? Address { get; set; }
    public string? Position { get; set; }
    public decimal? Salary { get; set; }

    public int? SupervisorId { get; set; }
    public int? DID { get; set; }
    public int? DepartmanagerId { get; set; }


    [InverseProperty("InstructorManage")]
    [ForeignKey("DepartmanagerId")]
    public Department? DepartmentManage { get; set; }



    [InverseProperty("Instructors")]
    [ForeignKey("DID")]
    public Department? Department { get; set; }



    [InverseProperty("Instructors")]
    [ForeignKey("SupervisorId")]
    public Instructor? Supervisor { get; set; }



    [InverseProperty("Supervisor")]
    public ICollection<Instructor> Instructors { get; set; }



    [InverseProperty("Instructor")]
    public ICollection<InstructorSubject> InstructorSubjects { get; set; }
}
