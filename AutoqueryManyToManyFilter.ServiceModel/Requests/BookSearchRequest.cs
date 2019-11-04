using System;
using System.Collections.Generic;
using System.Text;
using AutoqueryManyToManyFilter.ServiceModel.Dto;
using AutoqueryManyToManyFilter.ServiceModel.Entities;
using ServiceStack;

namespace AutoqueryManyToManyFilter.ServiceModel.Requests
{
    public class BookSearchRequest : QueryDb<Book, BookProduct>, IJoin<Book, BookBookFormat>
    {
    }
}
