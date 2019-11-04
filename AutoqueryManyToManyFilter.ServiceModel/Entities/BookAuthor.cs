using ServiceStack.DataAnnotations;

namespace AutoqueryManyToManyFilter.ServiceModel.Entities
{
    public class BookAuthor
    {
        [AutoIncrement]
        public int Id { get; set; }
        [ForeignKey(typeof(Book))]
        public int BookId { get; set; }
        [ForeignKey(typeof(Author))]
        public int AuthorId { get; set; }
    }
}
