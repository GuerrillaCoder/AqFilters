using ServiceStack.DataAnnotations;

namespace AutoqueryManyToManyFilter.ServiceModel.Entities
{
    public class BookFormat
    {
        //[AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
