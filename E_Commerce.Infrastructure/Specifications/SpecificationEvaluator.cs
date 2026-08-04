using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Specifications
{
    internal static class SpecificationEvaluator
    {
        // Spec => Query 

        public static IQueryable<TEntity> GreateQuery<TEntity, TKey>(IQueryable<TEntity> inputQuery , ISpecifications<TEntity,TKey>Spec) 
            where TEntity : BaseEntity<TKey>
        {
            // Entry Point 
            // Componenet 

            var query = inputQuery;

            if(Spec.Criteria != null)
            {
                query = query.Where(Spec.Criteria);
            }  


            if (Spec.IncludeExpressions.Any())
            {
                // foreach (var includeExpression in Spec.IncludeExpressions)
                //{
                // query = query.Include(includeExpression);
                //}
                // _dbContext .Products 
                // _dbContext .Products.include(Expression1)
                // _dbContext .Products,incude(Expression1).include(Expression2)

                query = Spec.IncludeExpressions.Aggregate(query, (current, NextExp) => current.Include(NextExp));
            }



            return query;
        }

    }
}
