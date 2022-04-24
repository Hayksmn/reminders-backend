using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reminders.Models;

namespace Reminders.Services
{
    
    public interface IReminderService
    {
        public Task<List<Reminder>> GetAllReminders();
        public Task<Reminder> GetReminder(Guid id);
        public Task<List<Reminder>> AddReminder(AddReminderRequest reminder);
        public Task<List<Reminder>> UpdateReminder(Reminder reminder);
        public Task<List<Reminder>> DeleteReminder(Guid id);


    }
}