using ServiceStack.DataAnnotations;

namespace AutoqueryManyToManyFilter.ServiceModel.Entities
{
    public class BookCategory
    {
        [AutoIncrement]
        public int Id { get; set; }
        [ForeignKey(typeof(Book))]
        public int BookId { get; set; }
        [ForeignKey(typeof(Category))]
        public int CategoryId { get; set; }
    }
}
