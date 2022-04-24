using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Reminders.Data;
using Reminders.Models;
using Reminders.Services;

namespace Reminders.Implementations
{
    public class ReminderService : IReminderService
    {
        private readonly DataContext _context;
        private const string DateFormat = "dd/MM/yyyy HH:mm:ss";
        private readonly CultureInfo _cultureInfo = CultureInfo.InvariantCulture;
            
        public ReminderService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Reminder>> GetAllReminders()
        {
            return await _context.Reminders.ToListAsync();
        }

        public async Task<Reminder> GetReminder(Guid id)
        {
            var reminder = await _context.Reminders.FindAsync(id) ??
                           throw new ObjectNotFoundException("Reminder with given ID does not exist");

            return reminder;
        }

        public async Task<List<Reminder>> AddReminder(AddReminderRequest reminderRequest)
        {
            var reminder = new Reminder
            {
                Text = reminderRequest.Text,
                ReminderDate =DateTime.ParseExact(reminderRequest.ReminderDate, DateFormat, _cultureInfo),
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            // Why use Add and SaveChangesAsync instead of AddAsync and SaveChanges
            _context.Reminders.Add(reminder);
            await _context.SaveChangesAsync();

            // await _context.Reminders.AddAsync(reminder);
            // _context.SaveChanges();
            return await _context.Reminders.ToListAsync();
        }

        public async Task<List<Reminder>> UpdateReminder(Reminder reminderRequest)
        {
            var reminder = await _context.Reminders.FindAsync(reminderRequest.Id) ??
                           throw new ObjectNotFoundException("Reminder with given ID does not exist");

            reminder.Text = reminderRequest.Text;
            reminder.IsSent = reminderRequest.IsSent;
            reminder.ReminderDate = reminderRequest.ReminderDate;
            reminder.ModifiedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return await _context.Reminders.ToListAsync();
        }

        public async Task<List<Reminder>> DeleteReminder(Guid id)
        {
            var reminder = await _context.Reminders.FindAsync(id) ??
                           throw new ObjectNotFoundException("Reminder with given ID does not exist");

            _context.Reminders.Remove(reminder);
            await _context.SaveChangesAsync();
            return await _context.Reminders.ToListAsync();
        }
    }
}