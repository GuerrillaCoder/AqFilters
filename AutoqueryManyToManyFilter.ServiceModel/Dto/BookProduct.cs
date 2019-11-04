using System;
using System.Collections.Generic;
using System.Text;
using AutoqueryManyToManyFilter.ServiceModel.Entities;

namespace AutoqueryManyToManyFilter.ServiceModel.Dto
{
    public class BookProduct
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int BookFormatId { get; set; }
        public List<Author> Authors { get; set; }
        public List<Category> Categories { get; set; }
        public List<BookFormat> BookFormats { get; set; }
    }
}
