using Library_Management_System.Entities;

namespace Library_Management_System.Models
{
    public class AuthorViewModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }  

        public DateTime DateOfBirth { get; set; }

        public List<string> BookTitles { get; set; }
    }
}
