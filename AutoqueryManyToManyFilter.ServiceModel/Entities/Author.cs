using ServiceStack.DataAnnotations;

namespace AutoqueryManyToManyFilter.ServiceModel.Entities
{
    public class Author
    {
        //[AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Natiionality { get; set; }
    }
}
