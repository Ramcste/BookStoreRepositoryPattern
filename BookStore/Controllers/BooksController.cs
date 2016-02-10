using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using BookStore.Models.DAL;

namespace BookStore.Controllers
{
    public class BooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private IBookRepository iBookRepository;


        public BooksController()
        {
            this.iBookRepository = new BookRepository(new BookContext());
        }

        // GET: Books
        public ActionResult Index()
        {
            var books = from book in iBookRepository.GetBooks()
                        select book;
            return View(books);
        }

        // GET: Books/Details/5
        public ActionResult Details(int id)
        {
          
            Book book = iBookRepository.GetBookById(id);

            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            return View(new Book());
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            try {
                if (ModelState.IsValid)
                {
                    iBookRepository.InsertBook(book);
                    iBookRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
   "Try again, and if the problem persists see your system administrator.");
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int id)
        {
           
            Book book = iBookRepository.GetBookById(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Book book)
        {
            try {
                if (ModelState.IsValid)
                {
                    iBookRepository.UpdateBook(book);
                    iBookRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
   "Try again, and if the problem persists see your system administrator.");
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int id ,bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Unable to save changes. Try again, " +
          "and if the problem persists see your system administrator.";
            }

            Book book = iBookRepository.GetBookById(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Book book = iBookRepository.GetBookById(id);
                iBookRepository.DeleteBook(id);
                iBookRepository.Save();
                return RedirectToAction("Index");
            }

            catch (DataException)
            {
                return RedirectToAction("Delete",
                           new System.Web.Routing.RouteValueDictionary {
        { "id", id },
        { "saveChangesError", true } });
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
