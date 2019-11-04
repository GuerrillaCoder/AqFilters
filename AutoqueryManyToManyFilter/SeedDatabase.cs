using AutoqueryManyToManyFilter.ServiceModel;
using ServiceStack;
using ServiceStack.Configuration;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoqueryManyToManyFilter.ServiceModel.Entities;
using Bogus;
using Bogus.DataSets;
using Microsoft.CodeAnalysis.Operations;

namespace AutoqueryManyToManyFilter
{
    public static class SeedDatabase
    {
        public static void Seed()
        {
            //database
            var dbFactory = HostContext.TryResolve<IDbConnectionFactory>();
            IAppSettings appSettings = HostContext.TryResolve<IAppSettings>();

            using (var db = dbFactory.Open())
            {
                if (!db.TableExists<Book>())
                {
                    Run(db);
                }
            }
        }

        public static void Run(System.Data.IDbConnection db)
        {

            db.CreateTableIfNotExists<Book>();
            db.CreateTableIfNotExists<Author>();
            db.CreateTableIfNotExists<Category>();
            db.CreateTableIfNotExists<BookFormat>();
            db.CreateTableIfNotExists<BookAuthor>();
            db.CreateTableIfNotExists<BookCategory>();
            db.CreateTableIfNotExists<BookBookFormat>();

            var formats = new List<BookFormat>()
                {
                     new BookFormat{ Id = 1, Name = "Ebook" },
                     new BookFormat{ Id = 2, Name = "Paperback" },
                     new BookFormat{ Id = 3, Name = "Hardcover" },
                     new BookFormat{ Id = 4, Name = "PDF" }
                };

            //db.SaveAll(formats);

            var faker = new Faker();

            var catNames = new HashSet<string>();
            
            for (int i = 0; i <= 300; i++)
            {
                catNames.Add(string.Join(' ', faker.Commerce.Categories(faker.Random.Int(1, 4))));
            }

            var cats = new List<Category>();
            var catIndex = 1;
            catNames.Each(x => cats.Add(new Category(){ Id = catIndex++, Name = x }));

            var authorFake =
                new Faker<Author>()
                    .RuleFor(b => b.Id, f => f.IndexFaker)
                    .RuleFor(a => a.Name, f => f.Person.FirstName + " " + f.Person.LastName)
                    .RuleFor(a => a.Age, f => f.Random.Int(20, 70))
                    .RuleFor(b => b.Natiionality, f => f.Address.Country());

            var authors = authorFake.Generate(10000);

            //db.SaveAll(authors);

            var test = faker.PickRandom(formats, faker.Random.Number(1, 4)).ToList();

            var bookFake =
                    new Faker<Book>()
                        .RuleFor(b => b.Id, f => f.IndexFaker)
                        .RuleFor(b => b.Title, f=> f.Commerce.ProductName() + " " + f.Commerce.ProductName())
                        .RuleFor(b => b.BookFormats, f => f.PickRandom(formats, f.Random.Number(1,4)).ToList())
                        .RuleFor(b => b.Authors, f => f.PickRandom(authors, f.Random.Number(1, 4)).ToList())
                        .RuleFor(b => b.Categories, f => f.PickRandom(cats, f.Random.Number(1, 4)).ToList())
                        // Library appears to be broken when referencing already generated properties
                        //.RuleFor(b => b.BookBookFormats, (f, b) => b.BookFormats.Select(bf => new BookBookFormat { BookFormatId = bf.Id, BookId = b.Id }).ToList())
                        //.RuleFor(b => b.BookAuthors, (f, b) => b.Authors.Select(ba => new BookAuthor { AuthorId = ba.Id, BookId = b.Id }).ToList())
                        //.RuleFor(b => b.BookCategories, (f, b) => b.Categories.Select(bc => new BookCategory { BookId = b.Id, CategoryId = bc.Id }).ToList())
                        ;


            var books = bookFake.Generate(20000);

            foreach (var book in books)
            {
                book.BookAuthors = book.Authors
                    .Select(a => new BookAuthor {BookId = book.Id, AuthorId = a.Id}).ToList();
                book.BookCategories = book.Categories
                    .Select(c => new BookCategory {BookId = book.Id, CategoryId = c.Id}).ToList();
                book.BookBookFormats = book.BookFormats
                    .Select(bf => new BookBookFormat
                    {
                        BookId = book.Id,
                        BookFormatId = bf.Id,
                        Price = Convert.ToDecimal(faker.Commerce.Price(0.1M, 300M, 2))
                    }).ToList();
            }


            db.SaveAll(authors);
            db.SaveAll(formats);
            db.SaveAll(cats);
            db.SaveAll(books);
            books.Each(db.SaveAllReferences);

        }

    }
}
