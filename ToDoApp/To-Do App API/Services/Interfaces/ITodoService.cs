using Concerns;

namespace Services.Interfaces
{
    public interface ITodoService
    {
        public List<TodoTask> GetAllTasksOfUser(int userId);

        public List<TodoTask> GetTasksByStatus(int userId, int statusId);

        public bool AddTask(TodoTask newTask);

        public bool ChangeStatus(int taskId, string completedDate);

        public bool UpdateTask(TodoTask editingTask);

        public bool DeleteTask(int taskId);

        public bool DeleteAllTasks(int userId);
    }
}
