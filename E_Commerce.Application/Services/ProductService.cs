using AutoMapper;
using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Products;
using E_Commerce.Application.Specifications;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services
{
    internal class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork , IMapper  mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<IReadOnlyList<BrandDto>>> GetAllBrandsAsync(CancellationToken ct = default)
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand , int>().GetAllAsync(ct);
            // Maping => AutoMappar

            var data = _mapper.Map<IReadOnlyList<BrandDto>>(brands);

            return Result <IReadOnlyList<BrandDto>>.Ok(data);
        }


        public async Task<Result<IReadOnlyList<TypeDto>>> GetAllTypesAsync(CancellationToken ct = default)
        {
            var Types = _mapper.Map<IReadOnlyList<TypeDto>>(await _unitOfWork.GetRepository<ProductType , int >().GetAllAsync(ct));

            return Result <IReadOnlyList<TypeDto>>.Ok(Types);
        }

        public  async Task<Result<ProductDto>> GetProductByIdAsync(int id, CancellationToken ct = default)
        {
            var spec = new ProductWithTypeAndBrandSpec(id);

            var Product =await _unitOfWork.GetRepository<Product , int>().GetByIdAsync(spec, ct);

            if (Product == null)
                return Error.NotFound("Product.NotFound ", $"Product With Id {id} Is NotFound ");

            return _mapper.Map<ProductDto>(Product);
        }

        public async Task<Result<PaginatedResult<ProductDto>>> GetAllProductsAsync(ProductQueryParams queryParams, CancellationToken ct = default)
        {
            var spec = new ProductWithTypeAndBrandSpec(queryParams);

            var Products =await _unitOfWork.GetRepository<Product ,  int>().GetAllAsync(spec);

            var data = _mapper.Map<IReadOnlyList<ProductDto>>(Products);

            var countSpec = new ProductCountSpecifications(queryParams);
            var countOfAllProducts =await _unitOfWork.GetRepository<Product, int>().CountAsync(countSpec);
            var result = new PaginatedResult<ProductDto>(queryParams.PageIndex, queryParams.pageSize, countOfAllProducts, data);

            return Result<PaginatedResult<ProductDto>>.Ok(result);


        }
    }
}
