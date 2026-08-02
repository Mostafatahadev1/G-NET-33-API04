using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.DataSeeding
{
    internal class CatalogDataSeeder(StoreDbContext dbContext, ILogger<CatalogDataSeeder> Logger) : IDataSeeder
    {
        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            try
            {
                var PindingMigrations = await dbContext.Database.GetPendingMigrationsAsync(ct);

                if (PindingMigrations.Any())

                    await dbContext.Database.MigrateAsync(ct);

                // seeding 

                // Path
                // "D:\backend ASP.NET\Asp.net Route course\Aliaa Tark\09 API\Session 01-20260618T120108Z-3-001\Session 01\E_Commerce\E_Commerce.API\bin\Debug\net8.0\DataSeed\products.json"

                var SeedRoot = Path.Combine(AppContext.BaseDirectory, "DataSeed");


                await SeedIfEmptyAsync<ProductBrand,int>(SeedRoot, "brands.json", ct);

                await SeedIfEmptyAsync<ProductType,int>(SeedRoot,"types.json",ct);

                await SeedIfEmptyAsync<Product, int>(SeedRoot, "Products.json", ct);

               int result =  await dbContext.SaveChangesAsync(ct);

                if(result > 0 )
                    Logger.LogInformation($"{result} Records Seeded Successfully");

                else
                    Logger.LogInformation("No Records Seeded");
            }
            catch
            { 
            }
        }


        private async Task SeedIfEmptyAsync<T, TKey>(
            string rootPath,
            string fileName,
            CancellationToken ct)
            where T : BaseEntity<TKey>
        {
            if (await dbContext.Set<T>().AnyAsync(ct))
            {
                Logger.LogInformation("Table Already Has Data");
                return;
            }

            var filePath = Path.Combine(rootPath, fileName);

            if (!File.Exists(filePath))
            {
                Logger.LogWarning($"File {filePath} Not Found");
                return;
            }


            using var fileStream = File.OpenRead(filePath);


            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var items = await JsonSerializer.DeserializeAsync<List<T>>(fileStream, options, ct);

            if (items?.Any() ?? false)
                dbContext.Set<T>().AddRange(items);
        }
    }


}

