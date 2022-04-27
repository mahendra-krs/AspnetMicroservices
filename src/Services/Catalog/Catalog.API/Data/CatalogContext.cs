using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            var configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var client = new MongoClient(configBuilder.GetSection("DatabaseSettings")["ConnectionString"]);
            var database = client.GetDatabase(configBuilder.GetSection("DatabaseSettings")["DatabaseName"]);
            Products = database.GetCollection<Product>(configBuilder.GetSection("DatabaseSettings")["CollectionName"]);

            CatalogContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}
