using TaskBack.Models.Entities;

namespace TaskBack.Models.Repositories.ReminderRepo
{
    public interface IReminderRepository
    {
        Task<IEnumerable<Reminder>> GetAllReminders();
        Task<Reminder> GetReminderById(int id);
        Task AddReminder(Reminder reminder);
        Task UpdateReminder(Reminder reminder);
        Task DeleteReminder(int id);
    }
}
