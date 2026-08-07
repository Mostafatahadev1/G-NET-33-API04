using E_Commerce.Application.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Common
{
    public sealed class PaginatedResult<TEntity>
    {
        private int v;
        private IReadOnlyList<ProductDto> data;

        public PaginatedResult(int pageIndex, int pageSize, int count, IReadOnlyList<ProductDto> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = (IReadOnlyList<TEntity>?)data;
        }

        public int PageIndex { get; }

        public int PageSize { get; }

        public int Count { get; }

        public IReadOnlyList <TEntity> Data { get;}
    }
}
