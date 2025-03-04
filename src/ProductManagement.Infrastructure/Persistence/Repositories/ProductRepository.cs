using MongoDB.Driver;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Repositories;
using ProductManagement.Domain.ValueObjects;
using ProductManagement.Infrastructure.Persistence.Documents;

namespace ProductManagement.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly MongoDbContext _context;

    public ProductRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<Product> GetByIdAsync(ProductId id, CancellationToken cancellationToken = default)
    {
        var document = await _context.Products
            .Find(p => p.Id == id.Value)
            .FirstOrDefaultAsync(cancellationToken);

        return document != null ? MapToEntity(document) : null;
    }

    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var documents = await _context.Products
            .Find(_ => true)
            .ToListAsync(cancellationToken);

        return documents.Select(MapToEntity);
    }

    public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        var document = MapToDocument(product);
        await _context.Products.InsertOneAsync(document, null, cancellationToken);
    }

    public async Task UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        var document = MapToDocument(product);
        await _context.Products.ReplaceOneAsync(
            p => p.Id == product.Id.Value,
            document,
            new ReplaceOptions { IsUpsert = false },
            cancellationToken);
    }

    public async Task DeleteAsync(ProductId id, CancellationToken cancellationToken = default)
    {
        await _context.Products.DeleteOneAsync(
            p => p.Id == id.Value,
            cancellationToken);
    }

    public async Task<bool> ExistsBySkuAsync(Sku sku, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .Find(p => p.Sku == sku.Value)
            .AnyAsync(cancellationToken);
    }

    private Product MapToEntity(ProductDocument document)
    {
        var productId = ProductId.Create(document.Id);
        var price = Price.Create(document.Price, document.Currency);
        var sku = Sku.Create(document.Sku);

        var product = Product.Create(
            productId,
            document.Name,
            document.Description,
            price,
            sku,
            document.StockQuantity
        );

        // Use reflection to set the CreatedAt and UpdatedAt fields directly
        typeof(Product).GetProperty("CreatedAt")
            .SetValue(product, document.CreatedAt);

        if (document.UpdatedAt.HasValue)
        {
            typeof(Product).GetProperty("UpdatedAt")
                .SetValue(product, document.UpdatedAt);
        }

        return product;
    }

    private ProductDocument MapToDocument(Product product)
    {
        return new ProductDocument
        {
            Id = product.Id.Value,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price.Amount,
            Currency = product.Price.Currency,
            Sku = product.Sku.Value,
            StockQuantity = product.StockQuantity,
            IsAvailable = product.IsAvailable,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt
        };
    }
}