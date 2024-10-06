using School.Data.Entities;

namespace School.Service.Abstracts;
public interface IDepartmentService
{
    Task<Department> GetDepartmentByIdAsync(int id);
    Task<bool> IsDepartmentExist(int departmentId);

}
