using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers;

public class UserController : Controller
{
    public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();

        // GET: User
        public ActionResult Index()
        {
            // Load users data into userlist
            if (!userlist.Any())
            {
                userlist.AddRange(new List<User>
                {
                    new User { Id = 1, Name = "John Doe", Email = "john.doe@example.com" },
                    new User { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com" },
                    new User { Id = 3, Name = "Alice Johnson", Email = "alice.johnson@example.com" }
                });
            }

            return View(userlist);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View(new User());
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                user.Id = userlist.Any() ? userlist.Max(u => u.Id) + 1 : 1;
                userlist.Add(user);
                TempData["Message"] = "User created successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                TempData["Error"] = "User not found!";
                return NotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            // This method is responsible for handling the HTTP POST request to update an existing user with the specified ID.
            // It receives user input from the form submission and updates the corresponding user's information in the userlist.
            // If successful, it redirects to the Index action to display the updated list of users.
            // If no user is found with the provided ID, it returns a HttpNotFoundResult.
            // If an error occurs during the process, it returns the Edit view to display any validation errors.
            if (ModelState.IsValid)
            {
                var existingUser = userlist.FirstOrDefault(u => u.Id == id);
                if (existingUser == null)
                {
                    TempData["Error"] = "User not found!";
                    return NotFound();
                }
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                TempData["Message"] = "User updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            // Implement the Delete method here
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                TempData["Error"] = "User not found!";
                return NotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            // Implement the Delete method (POST) here
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                TempData["Error"] = "User not found!";
                return NotFound();
            }
            userlist.Remove(user);
            TempData["Message"] = "User deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
}
