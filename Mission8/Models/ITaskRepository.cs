namespace Mission8.Models
{
    public interface ITaskRepository
    {
        IQueryable<TaskItem> Tasks { get; }
        void SaveTask(TaskItem task);
        void DeleteTask(TaskItem task);
    }
}
