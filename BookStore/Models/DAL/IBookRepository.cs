using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models.DAL
{
    public interface IBookRepository: IDisposable
    {
        IEnumerable<Book> GetBooks();
        Book GetBookById(int BookId);
        void InsertBook(Book book);
        void DeleteBook(int BookId);

        void UpdateBook(Book book);
        void Save();

      
    }
}