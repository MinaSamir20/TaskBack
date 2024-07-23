using Microsoft.AspNetCore.Mvc;
using TaskBack.Models.Entities;
using TaskBack.Models.Repositories.ReminderRepo;

namespace TaskBack.Controllers
{
    public class RemindersController(IReminderRepository repository) : Controller
    {
        private readonly IReminderRepository _repository = repository;

        public async Task<IActionResult> Index()
        {
            var reminders = await _repository.GetAllReminders();
            return View(reminders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reminder reminder)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddReminder(reminder);
                return RedirectToAction(nameof(Index));
            }
            return View(reminder);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var reminder = await _repository.GetReminderById(id);
            if (reminder == null)
            {
                return NotFound();
            }
            return View(reminder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Reminder reminder)
        {
            if (id != reminder.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _repository.UpdateReminder(reminder);
                return RedirectToAction(nameof(Index));
            }
            return View(reminder);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var reminder = await _repository.GetReminderById(id);
            if (reminder == null)
            {
                return NotFound();
            }
            return View(reminder);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteReminder(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
