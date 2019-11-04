﻿using System;
using System.Collections.Generic;
using System.Text;
using AutoqueryManyToManyFilter.ServiceModel.Entities;
using AutoqueryManyToManyFilter.ServiceModel.Requests;
using ServiceStack;
using ServiceStack.Logging;
using ServiceStack.OrmLite;

namespace AutoqueryManyToManyFilter.ServiceInterface
{
    public class CategoryBookSearchService : Service
    {
        public IAutoQueryDb AutoQuery { get; set; }
        private static ILog Log = LogManager.GetLogger(typeof(CategoryBookSearchService));

        public object Any(CategoryBookSearchRequest request)
        {
            var q = AutoQuery.CreateQuery(request, base.Request);

            if (q.Params.Count < 1) q.Where(x => 1 == 1);

            var authorRequest = new AuthorRequestOr().PopulateWith(request);
            var authorAuto = AutoQuery.CreateQuery(authorRequest, base.Request)
                .Join<BookAuthor>()
                .Select<BookAuthor>(x => x.BookId)
                .ClearLimits();

            var catRequest = new CategoryRequest().PopulateWith(request);
            var catAuto = AutoQuery.CreateQuery(catRequest, base.Request)
                .Join<BookCategory>()
                .Select<BookCategory>(x => x.BookId)
                .ClearLimits();


           
            if (catAuto.Params.Count > 0) q.And<Book>(x => Sql.In(x.Id, catAuto));
            if (authorAuto.Params.Count > 0) q.And<Book>(x => Sql.In(x.Id, authorAuto));

            var results = AutoQuery.Execute(request, q);

            return results;
        }


    }
}