using Microsoft.AspNetCore.Mvc;
using TaskBack.Models.Entities;
using TaskBack.Models.Repositories.DepartmentRepo;

namespace TaskBack.Controllers
{
    public class DepartmentsController(IDepartmentRepository repository) : Controller
    {

        private readonly IDepartmentRepository _repository = repository;

        public async Task<IActionResult> Index()
        {
            var departments = await _repository.GetAllDepartments();
            return View(departments);
        }

        public async Task<IActionResult> Details(int id)
        {
            var department = await _repository.GetDepartmentById(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddDepartment(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var department = await _repository.GetDepartmentById(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Department department)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _repository.UpdateDepartment(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var department = await _repository.GetDepartmentById(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteDepartment(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
