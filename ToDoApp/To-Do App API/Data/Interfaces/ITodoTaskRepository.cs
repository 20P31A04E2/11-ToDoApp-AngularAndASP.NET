using Repository.DataConcerns;

namespace Repository.Interfaces
{
    public interface ITodoTaskRepository
    {
        public List<TodoTask> GetAllTasksOfUser(int userId);

        public List<TodoTask> GetTasksByStatus(int userId, int statusId);

        public bool AddTask(TodoTask newtask);

        public bool ChangeStatus(int taskId, string completedDate);

        public bool UpdateTask(TodoTask editingTask);

        public bool DeleteTask(int taskId);

        public bool DeleteAllTasks(int userId);
    }
}
