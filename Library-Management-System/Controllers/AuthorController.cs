using Library_Management_System.Entities;
using Microsoft.AspNetCore.Mvc;
using Library_Management_System.Models;


namespace Library_Management_System.Controllers
{
    public class AuthorController : Controller
    {

        static List<AuthorEntity> _authors = new List<AuthorEntity>()
        {
           
        new AuthorEntity{Id = 1, FirstName = "Omer", LastName = "Seyfettin", DateOfBirth = new DateTime(1884, 03, 11, 0, 0, 0)},
      new AuthorEntity{Id = 2, FirstName = "Resat Nuri", LastName= " Guntekin", DateOfBirth = new DateTime(1889, 11, 25, 0, 0, 0)},
      new AuthorEntity{Id = 3, FirstName = "Paulo", LastName= "Coelho", DateOfBirth = new DateTime(1947, 08, 24, 0, 0, 0)}
         };


        // List of authors

        public IActionResult AuthorList()
        {
            var viewModel = _authors.Where(x => x.IsDeleted == false).Select(x => new AuthorViewModel
            {

                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                DateOfBirth = x.DateOfBirth
            }).ToList();

            return View(viewModel);
        }


        // Creating a new Author

        [HttpGet]
        public IActionResult AuthorCreate()
        {
            ViewBag.Authors = _authors;

            return View();
        }


        [HttpPost]
        public IActionResult AuthorCreate(AuthorViewModel formData)
        {

            if (!ModelState.IsValid)
            {
                return View(formData);
            }

            int maxId = _authors.Max(x => x.Id);

            var newAuthor = new AuthorEntity()
            {
                Id = maxId + 1,
                FirstName = formData.FirstName,
                LastName = formData.LastName,
                DateOfBirth = formData.DateOfBirth


            };
            _authors.Add(newAuthor);

            return RedirectToAction("AuthorList");
        }

        // Updating author information

        [HttpGet]
        public IActionResult AuthorEdit(int id)
        {
            var author = _authors.Find(x => x.Id == id);

            var viewModel = new AuthorViewModel()
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                DateOfBirth = author.DateOfBirth

            };

            ViewBag.Authors = _authors;

            return View(viewModel);
        }


        [HttpPost]
        public IActionResult AuthorEdit(AuthorViewModel formData)
        {
            if (!ModelState.IsValid)
            {
                return View(formData);
            }

            var author = _authors.Find(x => x.Id == formData.Id);

            author.FirstName = formData.FirstName;
            author.LastName = formData.LastName;
            author.DateOfBirth = formData.DateOfBirth;


            return RedirectToAction("AuthorList");
        }


        // Display author information

        public IActionResult AuthorDetail(int id)
        {
            var author = _authors.Find(x => x.Id == id);

            var viewModel = new AuthorViewModel
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                DateOfBirth = author.DateOfBirth,

            };

            return View(viewModel);

        }

        // Author deletion

        public IActionResult AuthorDelete(int id)
        {
            var author = _authors.Find(x => x.Id == id);

            author.IsDeleted = true;

            return RedirectToAction("AuthorList");
        }

    }

}
