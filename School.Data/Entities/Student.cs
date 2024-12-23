﻿using School.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Data.Entities;
public class Student : LocalizableEntity
{
    public Student()
    {
        StudentSubjects = new HashSet<StudentSubject>();
    }
    [Key]
    public int StudID { get; set; }
    [StringLength(200)]
    public string? NameEn { get; set; }
    [StringLength(200)]
    public string? NameAr { get; set; }
    [StringLength(500)]
    public string? Address { get; set; }
    [StringLength(500)]
    public string? Phone { get; set; }
    public int? DID { get; set; }

    [ForeignKey("DID")]
    public Department? Department { get; set; }
    public ICollection<StudentSubject> StudentSubjects { get; set; }

}
