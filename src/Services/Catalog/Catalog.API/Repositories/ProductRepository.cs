using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System.Xml.Linq;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext catalogContext)
        {
            this._context = catalogContext;
        }
        public async Task CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var deletedResult = await _context.Products.DeleteOneAsync(p => p.Id == id);
            return deletedResult.IsAcknowledged && deletedResult.DeletedCount > 0;
        }

        public async Task<Product> GetProduct(string id)
        {
            return await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            return await _context.Products.Find(p => p.Category == categoryName).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            return await _context.Products.Find(p => p.Name == name).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.Find(p => true).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updatedResult = await _context.Products.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);
            return updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
        }
    }
}
