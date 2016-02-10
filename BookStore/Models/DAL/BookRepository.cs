using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookStore.Models.DAL
{
    public class BookRepository:IBookRepository
    {

        private BookContext context;

        public BookRepository(BookContext bookContext)
        {
            this.context = bookContext;
        } 

        public IEnumerable<Book> GetBooks()
        {
            return context.Books.ToList();
        }

        public Book GetBookById(int bookId)
        {
            return context.Books.Find(bookId);
        }

        public void InsertBook(Book book)
        {
            context.Books.Add(book);
        }

        public void DeleteBook(int bookId)
        {
            Book book = context.Books.Find(bookId);

            context.Books.Remove(book);
        }

        public void UpdateBook(Book book)
        {
            context.Entry(book).State = EntityState.Modified;
        }


        public void Save()
        {
            context.SaveChanges();
        }


        private bool disposed = false;


        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}