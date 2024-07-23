using Microsoft.EntityFrameworkCore;
using TaskBack.Models.Database;
using TaskBack.Models.Entities;

namespace TaskBack.Models.Repositories.DepartmentRepo
{
    public class DepartmentRepository(AppDbContext context) : IDepartmentRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            return await _context.Departments.Include(d => d.SubDepartments).ToListAsync();
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            return (await _context.Departments
                .Include(d => d.SubDepartments)
                .Include(d => d.ParentDepartment)
                .FirstOrDefaultAsync(d => d.Id == id))!;
        }

        public async Task AddDepartment(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDepartment(Department department)
        {
            _context.Entry(department).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            _context.Departments.Remove(department!);
            await _context.SaveChangesAsync();
        }
    }
}
