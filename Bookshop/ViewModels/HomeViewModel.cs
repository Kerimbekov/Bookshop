using Bookshop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookshop.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Book> IsBestseller { get; set; }
    }
}
