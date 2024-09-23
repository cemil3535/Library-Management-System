using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.Models
{
    public class BookEditViewModal
    {
        public int Id { get; set; }

        public string Title { get; set; }
      
        public string Genre { get; set; }
   
        public string ISBN { get; set; }
        
        public int CopiesAvailable { get; set; }

        public DateTime PublishDate { get; set; }

        public int AuthorId { get; set; }
    }
}
