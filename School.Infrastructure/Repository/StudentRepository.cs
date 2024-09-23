using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Infrastructure.Abstracts;
using School.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Infrastructure.Repository;
internal class StudentRepository : IStudentRepository
{
    #region Fields
    private readonly ApplicationDbContext _context;
    #endregion

    #region Constructors
    public StudentRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    #endregion

    #region Handle Functions
    public async Task<List<Student>> GetStudentListAsync()
    {
        return await _context.Students.ToListAsync();
    }
    #endregion

}
