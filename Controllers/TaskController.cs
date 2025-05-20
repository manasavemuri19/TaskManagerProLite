using Microsoft.AspNetCore.Mvc; //For using MVC components like Controller, IActionResult
using TaskManagerProLite.Models; //Importing the Model to use the TaskItem
using System.Collections.Generic; //To use List

namespace TaskManagerProLite.Controllers
{
    public class TaskController : Controller //Our class inherited from base class to make it handle HTTP requests.
    {
        static List<TaskItem> tasks = new List<TaskItem>();  //Only to store tasks temporarily before DB creation.

        // GET/Task/Index -> default action (or) (or) [Route("tasks/list")] using Route attribute
        public IActionResult Index()
        {
            return View(tasks); // Returns the list of tasks to the Index.cshtml view.
        }

        // HTTP Method Overloading -> a common MVC pattern called GET/POST pattern.
        // Get will give empty form, Post will receive form data and process it.

        // 1. Create

        // GET/Task/Create 
        public IActionResult Create()
        {
            return View(); // User will see a form to enter task details,i.e.,create.cshtml view.
        }
        // POST/Task/Create
        [HttpPost]
        public IActionResult Create(TaskItem task) //Receives the submitted form as TaskItem.
                                    // MVC will auto-bind form data to TaskItem properties based on name attributes.
        {
            if (!ModelState.IsValid) // if any validation errors, return form again with errors shown.
            {
                return View(task);
            }
            task.Id = tasks.Count + 1;           // Simple way to assign unique ID.
            tasks.Add(task);                    // Adds the task to your static list.
            return RedirectToAction("Index");  // After adding, redirects user to the task list page.
        }

        // 2. Update

        // GET/Task/Edit/1  //Load existing task data into form.
        public IActionResult Edit(int id) //Receives the id to update
        {
            //Searches the list to find the first task whose Id matches the one from URL.
            //Each task is stored in t.
            var task = tasks.FirstOrDefault(t => t.Id == id); 
            if (task == null)
                return NotFound(); //404 if not found

            return View(task); //If found, passes the task to the Edit.cshtml view. This pre-fills the form fields.
        }
        // POST/Task/Edit/1  //Take submitted data, validate, update in list, and redirect.
        [HttpPost]
        public IActionResult Edit(TaskItem updatedTask)
        {
            if (!ModelState.IsValid)
            {
                return View(updatedTask);
            }

            var existingTask = tasks.FirstOrDefault(t => t.Id == updatedTask.Id);
            if (existingTask == null)
                return NotFound();

            existingTask.Title = updatedTask.Title;
            existingTask.Description = updatedTask.Description;
            existingTask.Status = updatedTask.Status;

            return RedirectToAction("Index");
        }

        // 3. Delete

        // GET: Task/Delete/5
        public IActionResult Delete(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound();

            return View(task); // Show confirmation page
        }

        // POST: Task/Delete/5
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                tasks.Remove(task);
            }
            return RedirectToAction("Index");
        }

        // View handles the form; Controller does the processing and redirecting.

    }
}
