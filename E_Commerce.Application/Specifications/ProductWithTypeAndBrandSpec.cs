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
        public ProductWithTypeAndBrandSpec(int? BrandId, int? TypeId)
        //  : base(P => (BrandId ==null || P.BrandId == BrandId) &&(TypeId == null || P.TypeId == TypeId)) true 

        : base(P => (!BrandId.HasValue  || P.BrandId == BrandId.Value) &&(!TypeId.HasValue || P.TypeId == TypeId.Value)) 


        // BrandId Is Not Null =?> p=>p.BrandId == BrandId

        // TypeId Is Not Null =?> p=>p.TypeId == TypeId

        // BrandId And TypeId Is Not Null =?> p=>p.BrandId == BrandId && p.TypeId == TypeId

        {
            AddInclude(P => P.productType);
            AddInclude(P => P.productBrand);


        }

        //Get By Id

        public ProductWithTypeAndBrandSpec(int id) : base(x => x.Id == id)
        {
            AddInclude(P => P.productType);
            AddInclude(P => P.productBrand);

        }
    }
}
