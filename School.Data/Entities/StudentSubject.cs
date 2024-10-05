using System.ComponentModel.DataAnnotations.Schema;

namespace School.Data.Entities;
public class StudentSubject
{
    public int StudID { get; set; }
    public int SubID { get; set; }

    [ForeignKey("StudID")]
    public Student? Student { get; set; }

    [ForeignKey("SubID")]
    public Subject? Subject { get; set; }

}
