using System;

namespace Reminders.Models
{
    public class Reminder
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Text { get; set; }
        public DateTime ReminderDate { get; set; }
        public bool IsSent { get; set; }

        public DateTime CreatedAt { get; set; }
        
        public DateTime ModifiedAt { get; set; }
    }
}