using Library_Management_System.Entities;
using Library_Management_System.Models;
using Microsoft.AspNetCore.Mvc;


namespace Library_Management_System.Controllers
{
    public class BookController : Controller
    {

        static List<BookEntity> _books = new List<BookEntity>()// authorId sonra ekleyecegim eksik
        {
            new BookEntity {Id = 1, Title = "Calikusu", AuthorId = 2, Genre = "Roman", ISBN = "9751027683", CopiesAvailable = 10000, PublishDate = new DateTime(1922, 1, 1,0, 0, 0)},
            new BookEntity {Id = 2, Title = "Simyaci", AuthorId = 3, Genre = "Fantastik Kurgu", ISBN = "333623788", CopiesAvailable = 5000, PublishDate = new DateTime(1988, 10, 8, 0, 0, 0)},
            new BookEntity {Id = 3, Title = "Basini Vermeyen Sehit", AuthorId = 1, Genre = "Kurgu", ISBN = "1176337683", CopiesAvailable = 20000, PublishDate = new DateTime(1917, 11, 22, 0, 0, 0)}
        };


        static List<AuthorEntity> _authors = new List<AuthorEntity>
        {
            new AuthorEntity{Id = 1, FullName = "Omer Seyfettin", DateOfBirth = new DateTime(1884, 03, 11, 0, 0, 0)},
            new AuthorEntity{Id = 2, FullName = "Resat Nuri Guntekin", DateOfBirth = new DateTime(1889, 11, 25, 0, 0, 0)},
            new AuthorEntity{Id = 3, FullName = "Paulo Coelho", DateOfBirth = new DateTime(1947, 08, 24, 0, 0, 0)}
        };


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
                                AuthorName = x.author.FullName,
                            }).ToList();

            return View(viewModel);
        }
        /*public IActionResult List()
        {
            var viewModel = _books.Where(x => x.IsDeleted == false).Select(x => new BookViewModel
            {

                Id = x.Id,
                Title = x.Title,
                Genre = x.Genre,
                ISBN = x.ISBN,
                CopiesAvailable = x.CopiesAvailable,
                PublishDate = x.PublishDate,
                AuthorName = _authors.FirstOrDefault(a => a.Id == x.AuthorId)?.FullName,
            }).ToList();

            return View(viewModel);
        }*/


        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Authors = _authors;
        
            return View();
        }


        [HttpPost]
        public IActionResult Create(BookCreateViewModel formData)
        {

            //ModelState.Remove("PublishDate");
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

            ViewBag.Authors = _authors;

            return View(viewModal);
        }

        [HttpPost]
        public IActionResult Edit(BookEditViewModal formData)
        {
            if (!ModelState.IsValid)
            {
                return View(formData);
            }

            var book = _books.Find(x=> x.Id == formData.Id);

            book.Title = formData.Title;
            book.Genre = formData.Genre;
            book.PublishDate = formData.PublishDate;
            book.CopiesAvailable = formData.CopiesAvailable;
            book.ISBN = formData.ISBN;
            book.AuthorId = formData.AuthorId;
           


            return RedirectToAction("List");
        }



        public IActionResult Delete(int id)
        {
            var book = _books.Find(x => x.Id == id);

            book.IsDeleted = true;

            return RedirectToAction("List");
        }

    }
}
