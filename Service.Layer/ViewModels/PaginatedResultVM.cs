﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Layer.ViewModels
{
    public class PaginatedResultVM<TEntity> where TEntity : class
    {
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<TEntity> Items { get; set; }
        public PaginatedResultVM(int totalCount, int pageIndex, int pageSize, IEnumerable<TEntity> items)
        {
            TotalCount = totalCount;
            PageIndex = pageIndex;
            PageSize = pageSize;
            Items = items;
        }
    }
}
