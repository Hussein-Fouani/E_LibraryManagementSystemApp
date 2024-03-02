using System.ComponentModel.DataAnnotations;

namespace E_LibraryManagementSystem.Models
{
    public class BookModel
    {
        public Guid Id { get; set; }
        
        public string BookName { get; set; }
      
        public string  BookAuthor { get; set; }
       
        public string BookPublication { get; set; }
        
        public double BookPrice { get; set; }
       
        public DateTime BookPurhcaseDate { get; set; }
        
        public int BookQuantity { get; set; }


       // public List<Student> students { get; set; }

    }
}
