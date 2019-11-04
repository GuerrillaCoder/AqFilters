using ServiceStack.DataAnnotations;

namespace AutoqueryManyToManyFilter.ServiceModel.Entities
{
    public class Category
    {
        //[AutoIncrement]
        public int Id { get; set; }
        [Index(Unique =true)]
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
