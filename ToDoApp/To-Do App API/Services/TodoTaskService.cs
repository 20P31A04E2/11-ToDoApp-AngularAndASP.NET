using AutoMapper;
using Concerns;
using Repository.Interfaces;
using Services.Interfaces;

namespace Services
{
    public class TodoTaskService : ITodoService
    {
        private readonly ITodoTaskRepository _todoTaskRepository;
        private readonly IMapper _mapper;
        const int TASKACTIVESTATUSID = 1;

        public TodoTaskService(ITodoTaskRepository todoTaskRepository, IMapper mapper)
        {
            _todoTaskRepository = todoTaskRepository;
            _mapper = mapper;
        }

        public List<TodoTask> GetAllTasksOfUser(int userId)
        {
            var tasks = _todoTaskRepository.GetAllTasksOfUser(userId);
            return tasks != null && tasks.Any() ? _mapper.Map<List<TodoTask>>(tasks) : new List<TodoTask>();
        }

        public List<TodoTask> GetTasksByStatus(int userId, int statusId)
        {
            var tasks = _todoTaskRepository.GetTasksByStatus(userId, statusId);
            return tasks != null && tasks.Any() ? _mapper.Map<List<TodoTask>>(tasks) : new List<TodoTask>();
        }

        public bool AddTask(TodoTask newTask)
        {
            if (newTask is not null)
            {
                Repository.DataConcerns.TodoTask convertedNewtask = _mapper.Map<Repository.DataConcerns.TodoTask>(newTask);
                convertedNewtask.StatusId = TASKACTIVESTATUSID;
                return _todoTaskRepository.AddTask(convertedNewtask);
            }
            return false;
        }

        public bool UpdateTask(TodoTask taskBeingUpdated)
        {
            if (taskBeingUpdated is not null)
            {
                Repository.DataConcerns.TodoTask convertedUpdatingTask = _mapper.Map<Repository.DataConcerns.TodoTask>(taskBeingUpdated);
                convertedUpdatingTask.StatusId = TASKACTIVESTATUSID;
                return _todoTaskRepository.UpdateTask(convertedUpdatingTask);
            }
            return false;
        }

        public bool ChangeStatus(int taskId, string completedDate)
        {
            return _todoTaskRepository.ChangeStatus(taskId, completedDate);
        }

        public bool DeleteTask(int taskId)
        {
            return _todoTaskRepository.DeleteTask(taskId);
        }

        public bool DeleteAllTasks(int userId)
        {
            return _todoTaskRepository.DeleteAllTasks(userId);
        }


    }
}
