using BogusStore.Domain.Products;
using BogusStore.Persistence;
using BogusStore.Shared.Products;
using Microsoft.EntityFrameworkCore;

namespace BogusStore.Services.Products;

public class ProductService : IProductService
{
    private readonly BogusDbContext dbContext;

    public ProductService(BogusDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<ProductResult.Index> GetIndexAsync(ProductRequest.Index request)
    {
        var query = dbContext.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Searchterm))
        {
            query = query.Where(x => x.Name.Contains(request.Searchterm, StringComparison.OrdinalIgnoreCase));
        }

        int totalAmount = await query.CountAsync();

        var items = await query
           .Skip((request.Page - 1) * request.PageSize)
           .Take(request.PageSize)
           .OrderBy(x => x.Id)
           .Select(x => new ProductDto.Index
           {
               Id = x.Id,
               Name = x.Name,
               Price = x.Price.Value,
           }).ToListAsync();

        var result = new ProductResult.Index
        {
            Products = items,
            TotalAmount = totalAmount
        };
        return result;
    }

    public async Task<ProductDto.Detail> GetDetailAsync(int productId)
    {
        ProductDto.Detail? product = await dbContext.Products.Select(x => new ProductDto.Detail
        {
            Id = x.Id,
            Name = x.Name,
            Price = x.Price.Value,
            Description = x.Description,
            Tags = x.Tags.Select(x => x.Name),
            CreatedAt = x.CreatedAt,
            UpdatedAt = x.UpdatedAt
        }).SingleOrDefaultAsync(x => x.Id == productId);

        if (product is null)
            throw new EntityNotFoundException(nameof(Product),productId);

        return product;
    }

    public async Task<int> CreateAsync(ProductDto.Mutate model)
    {
        if (await dbContext.Products.AnyAsync(x => x.Name == model.Name))
            throw new EntityAlreadyExistsException(nameof(Product), nameof(Product.Name), model.Name);

        Money price = new(model.Price);
        Product product = new(model.Name!, model.Description!, price);

        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync();

        return product.Id;
    }


    public async Task EditAsync(int productId, ProductDto.Mutate model)
    {
        Product? product = await dbContext.Products.SingleOrDefaultAsync(x => x.Id == productId);

        if (product is null)
            throw new EntityNotFoundException(nameof(Product), productId);

        Money price = new(model.Price);
        product.Name = model.Name!;
        product.Description = model.Description!;
        product.Price = price;

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int productId)
    {
        Product? product = await dbContext.Products.SingleOrDefaultAsync(x => x.Id == productId);

        if (product is null)
            throw new EntityNotFoundException(nameof(Product),productId);

        dbContext.Products.Remove(product);

        await dbContext.SaveChangesAsync();
    }

    public async Task AddTagAsync(int productId, int tagId)
    {
        Product? product = await dbContext.Products.SingleOrDefaultAsync(x => x.Id == productId);

        if (product is null)
            throw new EntityNotFoundException(nameof(Product), productId);

        Tag? tag = await dbContext.Tags.SingleOrDefaultAsync(x => x.Id == tagId);

        if (tag is null)
            throw new EntityNotFoundException(nameof(Tag), tagId);

        product.Tag(tag);

        await dbContext.SaveChangesAsync();
    }
}

