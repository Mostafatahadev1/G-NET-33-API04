using E_Commerce.Application.Common;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Specifications
{
    internal class ProductWithTypeAndBrandSpec : BaseSpecification<Product, int>
    {
        // Get All 
        public ProductWithTypeAndBrandSpec(ProductQueryParams queryParams)
        //  : base(P => (BrandId ==null || P.BrandId == BrandId) &&(TypeId == null || P.TypeId == TypeId)) true 

        : base(P => (!queryParams.BrandId.HasValue  || P.BrandId == queryParams.BrandId.Value) 
            &&(!queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId.Value)
            && (string.IsNullOrWhiteSpace(queryParams.SearchValue) || P.Name.ToLower().Contains(queryParams.SearchValue.ToLower()))) 


        // BrandId Is Not Null =?> p=>p.BrandId == BrandId

        // TypeId Is Not Null =?> p=>p.TypeId == TypeId

        // BrandId And TypeId Is Not Null =?> p=>p.BrandId == BrandId && p.TypeId == TypeId

        {
            AddInclude(P => P.productType);
            AddInclude(P => P.productBrand);


            switch (queryParams.Sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;

                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;

                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;

                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;

                default:
                    AddOrderBy(p => p.Id);
                    break;
            }

            ApplyPagination(queryParams.pageSize, queryParams.PageIndex);

        }

        //Get By Id

        public ProductWithTypeAndBrandSpec(int id) : base(x => x.Id == id)
        {
            AddInclude(P => P.productType);
            AddInclude(P => P.productBrand);

        }
    }
}
