using Repository.DataConcerns;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class TodoTaskRepository : ITodoTaskRepository
    {
        private readonly TodoContext _todoContext;

        const int TASKCOMPLETEDSTATUSID = 2;


        public TodoTaskRepository(TodoContext todoContext)
        {
            _todoContext = todoContext;
        }


        private IQueryable<TodoTask> JoinTables()
        {
            var query = from todoTask in _todoContext.TodoTasks
                        join status in _todoContext.Statuses on todoTask.StatusId equals status.StatusId
                        select new TodoTask
                        {
                            TaskId = todoTask.TaskId,
                            TaskName = todoTask.TaskName,
                            TaskDescription = todoTask.TaskDescription,
                            CreatedOn = todoTask.CreatedOn,
                            CompletedOn = todoTask.CompletedOn,
                            UserId = todoTask.UserId,
                            StatusId = todoTask.StatusId,
                            Status = todoTask.Status
                        };
            return query.AsQueryable();
        }

        public List<TodoTask> GetAllTasksOfUser(int userId)
        {
            var tasks = JoinTables().Where(t => t.UserId == userId);
            var t = tasks.ToList();
            return t;
        }

        public List<TodoTask> GetTasksByStatus(int userId, int statusId)
        {
            var tasks = JoinTables().Where(t => t.UserId == userId && t.StatusId == statusId);
            return tasks.ToList();
        }

        public bool AddTask(TodoTask newtask)
        {
            if (newtask is not null)
            {
                _todoContext.TodoTasks.Add(newtask);
                _todoContext.SaveChanges();
                return true;
            }
            return false;
        }


        public bool UpdateTask (TodoTask taskBeingUpdated)
        {
            var taskToUpdate = _todoContext.TodoTasks.SingleOrDefault(t => t.TaskId == taskBeingUpdated.TaskId);
            if (taskToUpdate is not null)
            {
                taskToUpdate.TaskName = taskBeingUpdated.TaskName;
                taskToUpdate.TaskDescription = taskBeingUpdated.TaskDescription;
                _todoContext.TodoTasks.Update(taskToUpdate);
                _todoContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool ChangeStatus(int taskId, string completedDate)
        {
            var taskToUpdate = _todoContext.TodoTasks.SingleOrDefault(t => t.TaskId == taskId);
            if (taskToUpdate is not null)
            {
                taskToUpdate.StatusId = TASKCOMPLETEDSTATUSID;
                taskToUpdate.CompletedOn = DateTime.ParseExact(completedDate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                _todoContext.TodoTasks.Update(taskToUpdate);
                _todoContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteTask(int taskId)
        {
            var task = _todoContext.TodoTasks.SingleOrDefault(task => task.TaskId == taskId);
            if (task is not null)
            {
                _todoContext.TodoTasks.Remove(task);
                _todoContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteAllTasks(int userId)
        {
            var tasks = _todoContext.TodoTasks.Where(task => task.UserId == userId);
            if (tasks is not null)
            {
                _todoContext.TodoTasks.RemoveRange(tasks);
                _todoContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
