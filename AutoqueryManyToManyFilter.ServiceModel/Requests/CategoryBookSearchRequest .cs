using System;
using System.Collections.Generic;
using System.Text;
using AutoqueryManyToManyFilter.ServiceModel.Dto;
using AutoqueryManyToManyFilter.ServiceModel.Entities;
using ServiceStack;

namespace AutoqueryManyToManyFilter.ServiceModel.Requests
{
    [QueryDb(QueryTerm.Or)]
    public class BroadCategoryBookSearchRequest : QueryDb<Book, BookProduct>, IJoin<Book, BookBookFormat>
    {
    }

    [QueryDb(QueryTerm.Or)]
    public class AuthorRequestOr : QueryDb<Author>
    {
    }
}
