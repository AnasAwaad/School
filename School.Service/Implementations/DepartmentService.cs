﻿using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Infrastructure.Abstracts;
using School.Service.Abstracts;

namespace School.Service.Implementations;
public class DepartmentService : IDepartmentService
{
    #region Fields
    private readonly IDepartmentRepository _departmentRepo;

    #endregion

    #region Constructor
    public DepartmentService(IDepartmentRepository departmentRepo)
    {
        _departmentRepo = departmentRepo;
    }


    #endregion

    #region Handle Functions
    public async Task<Department> GetDepartmentByIdAsync(int id)
    {
        var department = await _departmentRepo
            .GetTableNoTracking()
            .Where(d => d.DID == id)
            .Include(d => d.DepartmentSubjects)
                .ThenInclude(s => s.Subject)
            .Include(d => d.InstructorManage)
            .Include(d => d.Instructors)
            .FirstOrDefaultAsync();

        return department;
    }

    public async Task<bool> IsDepartmentExist(int departmentId)
    {
        return await _departmentRepo.GetTableNoTracking().AnyAsync(d => d.DID.Equals(departmentId));
    }
    #endregion
}
