using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Domain.NewFolder
{
    public class BookDto
    {

        public Guid Id { get; set; }
        public string BookName { get; set; }
        public string BookAuthor { get; set; }
        public string BookPublication { get; set; }
        public double BookPrice { get; set; }
        public string ISBN { get; set; }
        public string Language { get; set; }
        public int NumberOfCopies { get; set; }
        public bool IsAvailable { get; set; } 
        public string Genre { get; set; }

    }
}
