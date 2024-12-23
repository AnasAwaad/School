﻿using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Data.Helper;
using School.Infrastructure.Abstracts;
using School.Service.Abstracts;

namespace School.Service.Implementations;
public class StudentService : IStudentService
{
    #region Fields
    private readonly IStudentRepository _studentRepository;
    #endregion


    #region Constructor
    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }
    #endregion


    #region Handle Functions
    public async Task<List<Student>> GetStudentListAsync()
    {
        return await _studentRepository.GetStudentListAsync();
    }

    public async Task<Student> GetStudentWithDepartmentAsync(int id)
    {
        var student = await _studentRepository.GetTableNoTracking()
            .Include(s => s.Department)
            .Where(s => s.StudID == id)
            .FirstOrDefaultAsync();
        return student;
    }

    public async Task<string> AddStudentAsync(Student student)
    {
        // add new student
        await _studentRepository.AddAsync(student);
        return "Success";
    }

    public async Task<bool> IsStudentNameExistAsync(string name)
    {
        // check if the name is exists or not
        return await _studentRepository.GetTableNoTracking().AnyAsync(s => s.NameEn == name);
    }

    public async Task<bool> IsStudentNameExistAsync(string name, int id)
    {
        // check if the name is exists or not and execlude same person
        return await _studentRepository.GetTableNoTracking().AnyAsync(s => s.StudID != id && s.NameEn == name);
    }

    public async Task<string> EditStudentAsync(Student student)
    {
        await _studentRepository.UpdateAsync(student);
        return "Success";
    }

    public async Task<string> DeleteStudentAsync(Student student)
    {
        await _studentRepository.DeleteAsync(student);
        return "Success";
    }

    public async Task<Student> GetStudentByIdAsync(int id)
    {
        return await _studentRepository.GetByIdAsync(id);
    }

    public IQueryable<Student> GetStudentsAsQurable()
    {
        return _studentRepository.GetTableNoTracking().Include(s => s.Department);
    }

    public IQueryable<Student> GetFilteredStudentsAsQurable(StudentOrdering? orderBy, string? search)
    {
        var query = GetStudentsAsQurable();
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(s => s.NameEn.Contains(search) || s.Address.Contains(search));
        }

        if (orderBy is not null)
        {
            switch (orderBy)
            {
                case StudentOrdering.StudentId:
                    query = query.OrderBy(x => x.StudID);
                    break;
                case StudentOrdering.Name:
                    query = query.OrderBy(x => x.NameEn);
                    break;
                case StudentOrdering.Address:
                    query = query.OrderBy(x => x.Address);
                    break;
                case StudentOrdering.DepartmentName:
                    query = query.OrderBy(x => x.Department!.DNameEn);
                    break;
                default:
                    break;
            }
        }

        return query;
    }

    public IQueryable<Student> GetStudentsByDepartmentIdAsQurable(int departmentId)
    {
        return _studentRepository
            .GetTableNoTracking()
            .Where(s => s.DID == departmentId);

    }
    #endregion
}
