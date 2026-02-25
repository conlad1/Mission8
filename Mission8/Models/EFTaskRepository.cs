using Microsoft.EntityFrameworkCore;

namespace Mission8.Models
{
    public class EFTaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _context;

        public EFTaskRepository(TaskDbContext context)
        {
            _context = context;
        }

        public IQueryable<TaskItem> Tasks => _context.Tasks.Include(t => t.Category);

        public void SaveTask(TaskItem task)
        {
            if (task.TaskId == 0)
                _context.Tasks.Add(task);
            else
                _context.Tasks.Update(task);

            _context.SaveChanges();
        }

        public void DeleteTask(TaskItem task)
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }
    }
}
