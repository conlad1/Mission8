// ============================================================
//  TEMPORARY FILE - FOR LOCAL TESTING ONLY
//  Do NOT commit or push this file.
//  Your teammate (#4) will create the real controller.
// ============================================================

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mission8.Models;

namespace Mission8.Controllers;

public class TaskController : Controller
{
    // Fake in-memory data so the views have something to render
    private static List<TaskItem> _fakeTasks = new()
    {
        new TaskItem { TaskItemId = 1, TaskName = "Buy groceries",   QuadrantId = 1, CategoryId = 1, DueDate = DateTime.Today },
        new TaskItem { TaskItemId = 2, TaskName = "Plan vacation",   QuadrantId = 2, CategoryId = 3, DueDate = DateTime.Today.AddDays(7) },
        new TaskItem { TaskItemId = 3, TaskName = "Answer emails",   QuadrantId = 3, CategoryId = 3 },
        new TaskItem { TaskItemId = 4, TaskName = "Browse internet", QuadrantId = 4, CategoryId = 3 },
    };

    private static List<Category> _fakeCategories = new()
    {
        new Category { CategoryId = 1, CategoryName = "Home" },
        new Category { CategoryId = 2, CategoryName = "School" },
        new Category { CategoryId = 3, CategoryName = "Work" },
        new Category { CategoryId = 4, CategoryName = "Church" },
    };

    // ---- Quadrants View ----
    public IActionResult Quadrants()
    {
        // Only show incomplete tasks
        var incomplete = _fakeTasks.Where(t => !t.Completed).ToList();
        return View(incomplete);
    }

    // ---- Add Task (GET) ----
    public IActionResult Add()
    {
        LoadCategoryDropdown();
        return View("AddEditTask", new TaskItem());
    }

    // ---- Edit Task (GET) ----
    public IActionResult Edit(int id)
    {
        var task = _fakeTasks.FirstOrDefault(t => t.TaskItemId == id);
        if (task == null) return NotFound();

        LoadCategoryDropdown();
        return View("AddEditTask", task);
    }

    // ---- Save Task (POST) - handles both Add and Edit ----
    [HttpPost]
    public IActionResult Save(TaskItem task)
    {
        if (ModelState.IsValid)
        {
            if (task.TaskItemId == 0)
            {
                // New task
                task.TaskItemId = _fakeTasks.Max(t => t.TaskItemId) + 1;
                _fakeTasks.Add(task);
            }
            else
            {
                // Update existing
                var existing = _fakeTasks.FirstOrDefault(t => t.TaskItemId == task.TaskItemId);
                if (existing != null)
                {
                    existing.TaskName   = task.TaskName;
                    existing.DueDate    = task.DueDate;
                    existing.QuadrantId = task.QuadrantId;
                    existing.CategoryId = task.CategoryId;
                    existing.Completed  = task.Completed;
                }
            }
            return RedirectToAction("Quadrants");
        }

        LoadCategoryDropdown();
        return View("AddEditTask", task);
    }

    // ---- Mark as Completed (POST) ----
    [HttpPost]
    public IActionResult MarkComplete(int id)
    {
        var task = _fakeTasks.FirstOrDefault(t => t.TaskItemId == id);
        if (task != null) task.Completed = true;
        return RedirectToAction("Quadrants");
    }

    // ---- Delete (POST) ----
    [HttpPost]
    public IActionResult Delete(int id)
    {
        var task = _fakeTasks.FirstOrDefault(t => t.TaskItemId == id);
        if (task != null) _fakeTasks.Remove(task);
        return RedirectToAction("Quadrants");
    }

    // ---- Helper: populate the Category dropdown via ViewBag ----
    private void LoadCategoryDropdown()
    {
        ViewBag.Categories = new SelectList(_fakeCategories, "CategoryId", "CategoryName");
    }
}