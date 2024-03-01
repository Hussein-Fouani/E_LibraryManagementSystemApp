namespace E_LibraryApi.Models.Dto
{
    public class BookDto
    {
        public string BookName { get; set; }
        public string BookAuthor { get; set; }
        public string BookPublication { get; set; }
        public double BookPrice { get; set; }
        public DateTime BookPurhcaseDate { get; set; }
        public int BookQuantity { get; set; }
    }
}
