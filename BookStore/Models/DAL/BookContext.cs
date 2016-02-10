using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class BookContext:DbContext
    {
        public BookContext():base("name=BookStoreConnString")
        {

        }

        public DbSet<Book> Books { get; set; }
    }
}