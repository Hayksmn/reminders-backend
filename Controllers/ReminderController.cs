using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reminders.Models;
using Reminders.Services;

namespace Reminders.Controllers
{
    [Route("api/reminders")]
    [ApiController]
    public class ReminderController : ControllerBase
    {
        private readonly IReminderService _reminderService;

        public ReminderController(IReminderService reminderService)
        {
            _reminderService = reminderService;
        }

        [HttpGet("get-all")]
        public Task<ActionResult<List<Reminder>>> GetReminders()
        {
            return ExecuteCall(() => _reminderService.GetAllReminders());
        }

        [HttpGet("get/{id}")]
        public Task<ActionResult<Reminder>> GetReminder(string id)
        {
            return ExecuteCall(() => _reminderService.GetReminder(Guid.Parse(id)));
        }

        [HttpPost("add")]
        public Task<ActionResult<List<Reminder>>> AddReminder([FromBody] AddReminderRequest reminderRequest)
        {
            return ExecuteCall(()=> _reminderService.AddReminder(reminderRequest));
        }

        [HttpPut("update")]
        public Task<ActionResult<List<Reminder>>> UpdateReminder([FromBody] Reminder reminder)
        {
            return ExecuteCall(() => _reminderService.UpdateReminder(reminder));
        }

        [HttpDelete("delete/{id}")]
        public Task<ActionResult<List<Reminder>>> DeleteReminder(string id)
        {
            return ExecuteCall(() => _reminderService.DeleteReminder(Guid.Parse(id)));
        }

        private async Task<ActionResult<T>> ExecuteCall<T>(Func<Task<T>> block)
        {
            try
            {
                return Ok(await block());
            }
            catch (Exception e) when (e is NullReferenceException or ObjectNotFoundException)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Problem(e.Message);
            }
        }
    }
}