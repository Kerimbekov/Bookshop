using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookshop.Data.Interfaces;
using Bookshop.Data.Models;
using Bookshop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly ShoppingCart _shoppingCart;
        public ShoppingCartController(IBookRepository bookRepository,ShoppingCart shoppingCart)
        {
            _bookRepository = bookRepository;
            _shoppingCart = shoppingCart;
        }


        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            //foreach (var item in items)
            //    System.Diagnostics.Debug.WriteLine("sometext"+item.Drink.Name);
            _shoppingCart.ShoppingCartItems = items;

            var sCVM = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(sCVM);
        }
        [Authorize]
        public RedirectToActionResult AddToShoppingCart(int bookId)
        {
            var selectedBook = _bookRepository.Books.FirstOrDefault(p => p.BookId == bookId);
            if (selectedBook != null && selectedBook.InStock>0)
            {
                _shoppingCart.AddToCart(selectedBook, 1);
            }

            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int BookId)
        {
            var selectedBook = _bookRepository.Books.FirstOrDefault(p => p.BookId == BookId);
            if (selectedBook != null)
            {
                _shoppingCart.RemoveFromCart(selectedBook);
            }

            return RedirectToAction("Index");
        }
    }
}