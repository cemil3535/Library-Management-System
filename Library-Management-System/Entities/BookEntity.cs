

namespace Library_Management_System.Entities
{
    public class BookEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

       
        public string Genre { get; set; }

        public string ISBN { get; set; }

        public int CopiesAvailable { get; set; }
        
        public DateTime PublishDate { get; set; }

        public bool IsDeleted { get; set; }

        public int AuthorId { get; set; }
    }
}
