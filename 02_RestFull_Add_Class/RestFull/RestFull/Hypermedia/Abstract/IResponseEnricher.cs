﻿using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace RestFull.Hypermedia.Abstract
{
    public interface IResponseEnricher
    {
        bool CanEnrich(ResultExecutingContext context);
        Task Enrich(ResultExecutingContext context);
    }
}
