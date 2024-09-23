using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Library_Management_System.Models
{
    public class BookViewModel
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "Kitap Ismi Bilgisini Doldurmak Zorunludur.")]

        public string Title { get; set; }   

        public string AuthorName { get; set; }

        [Required(ErrorMessage = "Kitap Turu Bilgisini Doldurmak Zorunludur.")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "ISBN Bilgisini Doldurmak Zorunludur.")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Kopya Sayisi Bilgisini Doldurmak Zorunludur.Lutfen Sayi Olarak Giriniz")]
        public int CopiesAvailable { get; set; }

       
        public DateTime PublishDate { get; set; }

        public bool IsDeleted { get; set; }

        public int AuthorId { get; set; }




    }
}

