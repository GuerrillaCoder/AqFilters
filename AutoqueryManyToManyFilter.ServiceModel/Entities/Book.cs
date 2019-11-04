using System.Collections.Generic;
using ServiceStack.DataAnnotations;

namespace AutoqueryManyToManyFilter.ServiceModel.Entities
{
    public class Book
    {
        //[AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Author> Authors {get; set;}
        public List<Category> Categories { get; set; }
        public List<BookFormat> BookFormats { get; set; }

        [Reference]
        public List<BookCategory> BookCategories { get; set; }
        [Reference] 
        public List<BookAuthor> BookAuthors { get; set; }
        [Reference] 
        public List<BookBookFormat> BookBookFormats { get; set; }
    }
}
