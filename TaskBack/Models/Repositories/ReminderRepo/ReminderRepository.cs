using Microsoft.EntityFrameworkCore;
using TaskBack.Models.Database;
using TaskBack.Models.Entities;

namespace TaskBack.Models.Repositories.ReminderRepo
{
    public class ReminderRepository(AppDbContext context) : IReminderRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<IEnumerable<Reminder>> GetAllReminders()
        {
            return await _context.Reminders.ToListAsync();
        }

        public async Task<Reminder> GetReminderById(int id)
        {
            return (await _context.Reminders.FindAsync(id))!;
        }

        public async Task AddReminder(Reminder reminder)
        {
            await _context.Reminders.AddAsync(reminder);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReminder(Reminder reminder)
        {
            _context.Entry(reminder).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReminder(int id)
        {
            var reminder = await _context.Reminders.FindAsync(id);
            _context.Reminders.Remove(reminder!);
            await _context.SaveChangesAsync();
        }
    }
}
