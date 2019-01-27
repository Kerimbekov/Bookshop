using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookshop.Data.Interfaces;
using Bookshop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookRepository _bookRepository;
        public HomeController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }


        public ViewResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                IsBestseller = _bookRepository.IsBestseller
            };
            return View(homeViewModel);
        }
    }
}