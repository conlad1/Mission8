using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mission8.Models;

namespace Mission8.Controllers;

public class HomeController : Controller
{
    private readonly ITaskRepository _repository;
    private readonly TaskDbContext _context;

    public HomeController(ITaskRepository repository, TaskDbContext context)
    {
        _repository = repository;
        _context = context;
    }

    // GET: Display all INCOMPLETE tasks in Quadrant view
    public IActionResult Index()
    {
        var incompleteTasks = _repository.Tasks
            .Where(t => !t.Completed)
            .ToList();
        
        return View(incompleteTasks);
    }

    // GET: Show empty form to add new task
    [HttpGet]
    public IActionResult Add()
    {
        ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName");
        
        return View("AddEditTask", new TaskItem());
    }

    // GET: Show form to edit existing task
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var task = _repository.Tasks.FirstOrDefault(t => t.TaskId == id);
        
        if (task == null)
        {
            return NotFound();
        }
        
        ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName", task.CategoryId);
        
        return View("AddEditTask", task);
    }

    // POST: Save (Insert or Update) a task
    [HttpPost]
    public IActionResult Save(TaskItem task)
    {
        if (ModelState.IsValid)
        {
            _repository.SaveTask(task);
            return RedirectToAction("Index");
        }
        
        // If validation fails, reload the form with categories dropdown
        ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName", task.CategoryId);
        return View("AddEditTask", task);
    }

    // POST: Delete a task
    [HttpPost]
    public IActionResult Delete(int id)
    {
        var task = _repository.Tasks.FirstOrDefault(t => t.TaskId == id);
        
        if (task != null)
        {
            _repository.DeleteTask(task);
        }
        
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}