using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Entities;
public class Student
{
    public Student()
    {
        StudentSubjects=new HashSet<StudentSubject>();
    }
    [Key]
    public int StudID { get; set; }
    [StringLength(200)]
    public string Name { get; set; } = null!;
    [StringLength(500)]
    public string Address { get; set; } = null!;
    [StringLength(500)]
    public string Phone { get; set; } = null!;
    public int? DID { get; set; }

    [ForeignKey("DID")]
    public Department? Department { get; set; }
    public ICollection<StudentSubject> StudentSubjects { get; set; }

}
