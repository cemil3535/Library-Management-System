using Library_Management_System.Entities;
using Microsoft.AspNetCore.Mvc;
using Library_Management_System.Models;


namespace Library_Management_System.Controllers
{
    public class AuthorController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("List", "Book");
        }
    }
}
