using TaskBack.Models.Repositories.ReminderRepo;

namespace TaskBack.Models.services
{
    public class ReminderService(IServiceScopeFactory scopeFactory, ILogger<ReminderService> logger) : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
        private readonly ILogger<ReminderService> _logger = logger;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(CheckReminders!, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }

        private async void CheckReminders(object state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var reminderRepository = scope.ServiceProvider.GetRequiredService<IReminderRepository>();
                var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

                var reminders = (await reminderRepository.GetAllReminders())
                                .Where(r => !r.IsSent && r.ReminderDateTime <= DateTime.Now)
                                .ToList();

                foreach (var reminder in reminders)
                {
                    await emailSender.SendEmailAsync("user@example.com", reminder.Title!, "This is your reminder.");
                    reminder.IsSent = true;
                    await reminderRepository.UpdateReminder(reminder);
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose() => _timer?.Dispose();
    }
}
