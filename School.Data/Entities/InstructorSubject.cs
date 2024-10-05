using System.ComponentModel.DataAnnotations.Schema;

namespace School.Data.Entities;
public class InstructorSubject
{
    public int InstId { get; set; }
    public int SubID { get; set; }

    [InverseProperty("InstructorSubjects")]
    [ForeignKey("InstId")]
    public Instructor? Instructor { get; set; }


    [InverseProperty("InstructorSubjects")]
    [ForeignKey("SubID")]
    public Subject? Subject { get; set; }
}
