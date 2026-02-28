// ============================================================
//  TEMPORARY FILE - FOR LOCAL TESTING ONLY
//  Do NOT commit or push this file.
//  Your teammate (#1) will create the real models.
// ============================================================

using System.ComponentModel.DataAnnotations;

namespace Mission8.Models;

public class TaskItem
{
    public int TaskItemId { get; set; }

    [Required]
    public string TaskName { get; set; } = string.Empty;

    public DateTime? DueDate { get; set; }

    [Required]
    public int QuadrantId { get; set; }

    public int? CategoryId { get; set; }

    // Navigation property - links to the Category table
    public Category? Category { get; set; }

    public bool Completed { get; set; } = false;
}