using Microsoft.AspNetCore.Mvc;
using TaskManagerProLite.Models;
using System.Collections.Generic;

namespace TaskManagerProLite.Controllers
{
    public class TaskController : Controller
    {
        static List<TaskItem> tasks = new List<TaskItem>();

        public IActionResult Index()
        {
            return View(tasks);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TaskItem task)
        {
            task.Id = tasks.Count + 1;
            tasks.Add(task);
            return RedirectToAction("Index");
        }
    }
}
