using Bookshop.Data;
using Bookshop.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bookshop.Data
{
    public class DbInitializer
    {
        public static void Seed(IServiceProvider applicationBuilder)
        {
            AppDbContext context =
                applicationBuilder.GetRequiredService<AppDbContext>();

            if (!context.Categories.Any())
            {
                context.Categories.AddRange(Categories.Select(c => c.Value));
            }

            if (!context.Books.Any())
            {
                context.AddRange
                (
                    new Book
                    {

                        Name = "Fahrenheit 451",
                        Author = "Ray Bradbury",
                        EditionDate = 1953,
                        Pages = 345,
                        Category = Categories["Science-Fiction"],
                        IsBestseller = true,
                        InStock =5
                    },
                      new Book
                      {

                          Name = "1984",
                          Author = "Gearge Orwell",
                          EditionDate = 1949,
                          Pages = 313,
                          Category = Categories["Science-Fiction"],
                          IsBestseller = true,
                          InStock = 3
                      },
                        new Book
                        {

                            Name = "Spider-Man",
                            Author = "Stan Lee",
                            EditionDate = 1962,
                            Pages = 200,
                            Category = Categories["Comics"],
                            IsBestseller = true,
                            InStock = 12
                        }


                );
            }

            context.SaveChanges();
        }

        private static Dictionary<string, Category> categories;
        public static Dictionary<string, Category> Categories
        {
            get
            {
                if (categories == null)
                {
                    var genresList = new Category[]
                    {
                        new Category { CategoryName = "Science-Fiction" },
                        new Category { CategoryName = "Comics"}
                    };

                    categories = new Dictionary<string, Category>();

                    foreach (Category genre in genresList)
                    {
                        categories.Add(genre.CategoryName, genre);
                    }
                }

                return categories;
            }
        }
    }
}


