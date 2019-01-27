using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookshop.Data.Interfaces;
using Bookshop.Data.Models;
using Bookshop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop.Controllers
{
    public class BookController : Controller
    {
        IBookRepository _bookRepository;
        ICategoryRepository _categoryRepository;
        public BookController(ICategoryRepository categoryRepository,IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
        }
        public ViewResult List(string category)
        {
            string _category = category;
            IEnumerable<Book> books;

            string currentCategory = string.Empty;

            if (string.IsNullOrEmpty(category))
            {
                books = _bookRepository.Books.OrderBy(n => n.BookId);
                currentCategory = "All books";
            }
            else
            {
                if (string.Equals("Science-Fiction", _category, StringComparison.OrdinalIgnoreCase))
                {
                    books = _bookRepository.Books.Where(p => p.Category.CategoryName.Equals("Science-Fiction")).OrderBy(n => n.BookId);
                }
                else
                {
                    books = _bookRepository.Books.Where(p => p.Category.CategoryName.Equals("Comics")).OrderBy(n => n.BookId);
                }
                currentCategory = _category;
            }


            var bookListViewModel = new BookListViewModel
            {
                Books = books,
                CurrentCategory = currentCategory
            };

            return View(bookListViewModel);
        }

        public ViewResult Search(string searchString)
        {
            string _searchString = searchString;
            IEnumerable<Book> books;
            string currentCategory = string.Empty;

            if (string.IsNullOrEmpty(_searchString))
            {
                books = _bookRepository.Books.OrderBy(p => p.BookId);
            }
            else
            {
                books = _bookRepository.Books.Where(p => p.Name.ToLower().Contains(_searchString.ToLower()));
            }

            return View("~/Views/Book/List.cshtml", new BookListViewModel { Books = books, CurrentCategory = "All books" });
        }
    }
}