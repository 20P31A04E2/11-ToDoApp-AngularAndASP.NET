using Concerns;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodoTasksController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoTasksController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        private int? GetUserIdFromToken()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return null;
            }
            return int.Parse(userIdClaim.Value);
        }

        [HttpGet()]
        public IActionResult GetAllTask()
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return Unauthorized();
            }
            var tasks = _todoService.GetAllTasksOfUser(userId.Value);
            return Ok(tasks);
        }

        [HttpGet("{status}")]
        public IActionResult GetTasksUsingStatus(string status)
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return Unauthorized();
            }
            if (status == "Active")
            {
                int taskActiveStatusId = 1;
                var tasks = (_todoService.GetTasksByStatus(userId.Value, taskActiveStatusId));
                return Ok(tasks);
            }
            else
            {
                int taskCompletedStatusId = 2;
                var tasks = (_todoService.GetTasksByStatus(userId.Value, taskCompletedStatusId));
                return Ok(tasks);
            }
        }

        [HttpPost]
        public IActionResult AddNewTask([FromBody] TodoTask newTask)
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return Unauthorized();
            }
            newTask.UserId = userId.Value;
            try
            {
                var addedTask = _todoService.AddTask(newTask);
                if (addedTask)
                    return Ok();
                else
                    return StatusCode(500, "Failed to add task");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to add task");
            }
        }

        [HttpPut()]
        public IActionResult UpdateTask(TodoTask taskBeingUpdated)
        {
            var result = _todoService.UpdateTask(taskBeingUpdated);
            if (result)
                return Ok();
            else
                return NotFound();
        }

        [HttpPut("{taskId}/{completedDate}")]
        public IActionResult UpdateStatus(int taskId, string completedDate)
        {
            var result = _todoService.ChangeStatus(taskId, completedDate);
            if (result)
                return Ok();
            else
                return NotFound();
        }

        [HttpDelete("{taskId}")]
        public IActionResult Delete(int taskId)
        {
            var result = _todoService.DeleteTask(taskId);
            if (result)
                return Ok();
            else
                return NotFound();
        }

        [HttpDelete("DeleteAllTask")]
        public IActionResult DeleteTasks()
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return Unauthorized();
            }
            var result = _todoService.DeleteAllTasks(userId.Value);
            if (result)
                return Ok();
            else
                return NotFound();
        }
    }
}
