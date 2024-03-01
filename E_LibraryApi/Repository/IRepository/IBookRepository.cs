using E_LibraryApi.Models;
using E_LibraryApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace E_LibraryApi.Repository.IRepository
{
    public interface IBookRepository
    {
        Task<List<BookModel>> GetAllBooks(Expression<Func<BookModel,bool>> filter = null);
        Task<BookModel> GetBook(Expression<Func<BookModel,bool>> filter = null,bool tracked = true);
        Task<bool> BookExists (string bookName);
        Task CreateBook(BookModel book);
        Task UpdateBook(BookModel book);
        Task DeleteBook(BookModel book);
        Task Save(); 
    }
}
