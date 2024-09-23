﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Entities;
public class Subject
{
    public Subject()
    {
        StudentsSubjects = new HashSet<StudentSubject>();
        DepartmetsSubjects = new HashSet<DepartmetSubject>();
    }
    [Key]
    public int SubID { get; set; }

    [StringLength(500)]
    public string SubjectName { get; set; } = null!;
    public DateTime Period { get; set; }
    public ICollection<StudentSubject> StudentsSubjects { get; set; }
    public ICollection<DepartmetSubject> DepartmetsSubjects { get; set; }
}