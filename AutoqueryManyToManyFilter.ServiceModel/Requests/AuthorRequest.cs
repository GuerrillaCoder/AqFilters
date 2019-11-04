using System;
using System.Collections.Generic;
using System.Text;
using AutoqueryManyToManyFilter.ServiceModel.Entities;
using ServiceStack;

namespace AutoqueryManyToManyFilter.ServiceModel.Requests
{
    public class AuthorRequest : QueryDb<Author>
    {
    }
}
