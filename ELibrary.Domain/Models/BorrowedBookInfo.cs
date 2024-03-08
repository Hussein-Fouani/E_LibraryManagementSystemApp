using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Domain.Models
{
    public class BorrowedBookInfo
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public Guid BookId { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string ISBN { get; set; }
        public string Language { get; set; } = "en";
        public string BookPublication { get; set; }
        public double BookPrice { get; set; }
        public string BorrowDate { get; set; }
        public string? ReturnDate { get; set; }
    }
}
