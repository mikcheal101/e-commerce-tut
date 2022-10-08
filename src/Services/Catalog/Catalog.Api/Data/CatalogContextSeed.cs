using System;
using System.Collections.Generic;
namespace Catalog.Api.Data
{
    using MongoDB.Driver;
    using Catalog.Api.Entities;

    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existsProducts = productCollection.Find<Product>(prediction => true).Any<Product>();

            if (!existsProducts)
            {
                productCollection.InsertManyAsync(GetPreConfiguredProducts());
            }
        }

        private static IEnumerable<Product> GetPreConfiguredProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = "6340e182cca7df38393471c3",
                    Name = "Iphone X",
                    Summary = "Lorem Ipsum Summary",
                    Description = "Lorem Ipsum",
                    ImageFile = "product-1.png",
                    Price = 1800.99M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Id = "6340e182cca7df38393471c4",
                    Name = "Samsung 10",
                    Summary = "Lorem Ipsum Summary",
                    Description = "Lorem Ipsum",
                    ImageFile = "product-2.png",
                    Price = 810.99M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Id = "6340e182cca7df38393471c5",
                    Name = "Huawei Plus",
                    Summary = "Lorem Ipsum Summary",
                    Description = "Lorem Ipsum",
                    ImageFile = "product-3.png",
                    Price = 610.00M,
                    Category = "White Appliances"
                },
                new Product()
                {
                    Id = "6340e182cca7df38393471c6",
                    Name = "Xiami M9",
                    Summary = "Lorem Ipsum Summary",
                    Description = "Lorem Ipsum",
                    ImageFile = "product-4.png",
                    Price = 1210.00M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Id = "6340e182cca7df38393471c7",
                    Name = "HTC U11+ Plus",
                    Summary = "Lorem Ipsum Summary",
                    Description = "Lorem Ipsum",
                    ImageFile = "product-5.png",
                    Price = 825.75M,
                    Category = "White Appliances"
                },
                new Product()
                {
                    Id = "6340e182cca7df38393471c8",
                    Name = "LG G7 ThinQ",
                    Summary = "Lorem Ipsum Summary",
                    Description = "Lorem Ipsum",
                    ImageFile = "product-6.png",
                    Price = 300.50M,
                    Category = "Home Kitchen"
                }
            };
        }
    }
}