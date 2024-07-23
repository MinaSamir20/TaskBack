using TaskBack.Models.Entities;

namespace TaskBack.Models.Repositories.DepartmentRepo
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllDepartments();
        Task<Department> GetDepartmentById(int id);
        Task AddDepartment(Department department);
        Task UpdateDepartment(Department department);
        Task DeleteDepartment(int id);
    }
}
