using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Entities;
public partial class Department
{
    public Department()
    {
        Students = new HashSet<Student>();
        DepartmentSubjects = new HashSet<DepartmetSubject>();
    }
    [Key]
    public int DID { get; set; }
    [StringLength(500)]
    public string DName { get; set; } = null!;
    public ICollection<Student> Students { get; set; }
    public ICollection<DepartmetSubject> DepartmentSubjects { get; set; }
}