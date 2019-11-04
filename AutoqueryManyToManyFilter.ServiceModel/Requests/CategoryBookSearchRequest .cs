using System;
using System.Collections.Generic;
using System.Text;
using AutoqueryManyToManyFilter.ServiceModel.Dto;
using AutoqueryManyToManyFilter.ServiceModel.Entities;
using ServiceStack;

namespace AutoqueryManyToManyFilter.ServiceModel.Requests
{
    public class CategoryBookSearchRequest : QueryDb<Book, BookProduct>, IJoin<Book, BookBookFormat>
    {
        [QueryDbField(Term = QueryTerm.Or)]
        public string TitleContains { get; set; }
        [QueryDbField(Term = QueryTerm.Or)]
        public string AuthorNameContains { get; set; }
        [QueryDbField(Term = QueryTerm.Or)]
        public string AuthorNationalityContains { get; set; }
    }

    public class AuthorRequestOr : QueryDb<Author>
    {
        [QueryDbField(Term = QueryTerm.Or)]
        public string AuthorNameContains { get; set; }
        [QueryDbField(Term = QueryTerm.Or)]
        public string AuthorNationalityContains { get; set; }
    }
}
