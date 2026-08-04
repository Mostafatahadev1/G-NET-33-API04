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
        public ProductWithTypeAndBrandSpec()
        {
            AddInclude(P => P.productType);
            AddInclude(P => P.productBrand);


        }
    }
}
