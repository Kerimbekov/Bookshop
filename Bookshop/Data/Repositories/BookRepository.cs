using Bookshop.Data.Interfaces;
using Bookshop.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookshop.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _appDbContext;
        public BookRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Book> Books => _appDbContext.Books.Include(c => c.Category);
        public IEnumerable<Book> IsBestseller => _appDbContext.Books.Where(p => p.IsBestseller).Include(c => c.Category);

        public Book GetBookById(int bookId) => _appDbContext.Books.FirstOrDefault(p => p.BookId == bookId);
    }
}

