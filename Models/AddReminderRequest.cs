using System;

namespace Reminders.Models
{
    public class AddReminderRequest
    {
        public string Text { get; set; }
        public string ReminderDate { get; set; }
    }
}