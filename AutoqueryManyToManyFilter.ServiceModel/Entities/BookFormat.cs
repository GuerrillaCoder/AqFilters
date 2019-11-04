using ServiceStack.DataAnnotations;

namespace AutoqueryManyToManyFilter.ServiceModel.Entities
{
    public class BookBookFormat
    {
        [AutoIncrement]
        public int Id { get; set; }
        public decimal Price { get; set; }
        [ForeignKey(typeof(Book))]
        public int BookId { get; set; }
        [ForeignKey(typeof(BookFormat))]
        public int BookFormatId { get; set; }
    }
}
