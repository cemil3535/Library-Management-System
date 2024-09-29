using Library_Management_System.Entities;
using Library_Management_System.Models;
using Microsoft.AspNetCore.Mvc;


namespace Library_Management_System.Controllers
{
    public class BookController : Controller
    {  
       
        static List<BookEntity> _books = new List<BookEntity>()
        {
            new BookEntity {Id = 1, Title = "Calikusu", AuthorId = 2, Genre = "Roman", ISBN = "9751027683", CopiesAvailable = 10000, PublishDate = new DateTime(1922, 1, 1,0, 0, 0)},
            new BookEntity {Id = 2, Title = "Simyaci", AuthorId = 3, Genre = "Fantastik Kurgu", ISBN = "333623788", CopiesAvailable = 5000, PublishDate = new DateTime(1988, 10, 8, 0, 0, 0)},
            new BookEntity {Id = 3, Title = "Basini Vermeyen Sehit", AuthorId = 1, Genre = "Kurgu", ISBN = "1176337683", CopiesAvailable = 20000, PublishDate = new DateTime(1917, 11, 22, 0, 0, 0)}
        };



        public static List<AuthorEntity> _authors = new List<AuthorEntity>()
        {
            new AuthorEntity{Id = 1, FirstName = "Omer ", LastName = "Seyfettin", DateOfBirth = new DateTime(1884, 03, 11, 0, 0, 0)},
            new AuthorEntity{Id = 2, FirstName = "Resat Nuri ", LastName = "Guntekin", DateOfBirth = new DateTime(1889, 11, 25, 0, 0, 0)},
            new AuthorEntity{Id = 3, FirstName = "Paulo ", LastName = "Coelho", DateOfBirth = new DateTime(1947, 08, 24, 0, 0, 0)}
        };


        // Listing of books

        public IActionResult List()
        {
            var x = _books;

            var viewModel = _books.Where(x => x.IsDeleted == false)
                            .Join(
                            _authors,
                            book => book.AuthorId,
                            author => author.Id,
                            (book, author) => new { book, author }
                            )
                            .Select(x => new BookViewModel
                            {
                                Id = x.book.Id,
                                Title = x.book.Title,
                                Genre = x.book.Genre,
                                ISBN = x.book.ISBN,
                                CopiesAvailable = x.book.CopiesAvailable,
                                PublishDate = x.book.PublishDate,
                                AuthorName = x.author.FirstName + " " + x.author.LastName
                            }).ToList();
          

            

            return View(viewModel);
            
        }

        // Adding new books

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Authors = _authors.Select(a => new
            {
                Id = a.Id,
                FullName = $"{a.FirstName} {a.LastName}"
            }).ToList();

            return View();
        }



        [HttpPost]
        public IActionResult Create(BookCreateViewModel formData)
        {

            if (!ModelState.IsValid)
            {
                return View(formData);
            }

            int maxId = _books.Max(x => x.Id);

            var newBook = new BookEntity()
            {
                Id = maxId + 1,
                Title = formData.Title,
                Genre = formData.Genre,
                ISBN = formData.ISBN,
                CopiesAvailable = formData.CopiesAvailable,
                PublishDate = formData.PublishDate,
                AuthorId = formData.AuthorId

            };
            _books.Add(newBook);

            return RedirectToAction("List");
        }

        // Updating book information

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var book = _books.Find(x => x.Id == id);

            var viewModal = new BookEditViewModal()
            {
                Id = book.Id,
                Title = book.Title,
                Genre = book.Genre,
                ISBN = book.ISBN,
                CopiesAvailable = book.CopiesAvailable,
                PublishDate = book.PublishDate,
                AuthorId = book.AuthorId
            };

            ViewBag.Authors = _authors.Select(a => new
            {
                Id = a.Id,
                FullName = $"{a.FirstName} {a.LastName}"
            }).ToList();

            return View(viewModal);
        }



        [HttpPost]
        public IActionResult Edit(BookEditViewModal formData)
        {
            if (!ModelState.IsValid)
            {
                return View(formData);
            }

            var book = _books.Find(x => x.Id == formData.Id);

            book.Title = formData.Title;
            book.Genre = formData.Genre;
            book.PublishDate = formData.PublishDate;
            book.CopiesAvailable = formData.CopiesAvailable;
            book.ISBN = formData.ISBN;
            book.AuthorId = formData.AuthorId;



            return RedirectToAction("List");
        }

        // Display book information

        public IActionResult Detail(int id)
        {
            var book = _books.Find(x => x.Id == id);

            var viewModel = new BookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Genre = book.Genre,
                ISBN = book.ISBN,
                CopiesAvailable = book.CopiesAvailable,
                PublishDate = book.PublishDate,
                AuthorName = _authors.FirstOrDefault(a => a.Id == book.AuthorId)?.FirstName + " " + _authors.FirstOrDefault(a => a.Id == book.AuthorId)?.LastName
            };

            return View(viewModel);
        }

        // Book deletion

        public IActionResult Delete(int id)
        {
            var book = _books.Find(x => x.Id == id);

            book.IsDeleted = true;

            return RedirectToAction("List");
        }
       
    }

}
