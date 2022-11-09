using Microsoft.AspNetCore.Mvc;
using BogusStore.Shared.Products;
using Swashbuckle.AspNetCore.Annotations;

namespace BogusStore.Server.Controllers.Products;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService productService;

    public ProductController(IProductService productService)
    {
        this.productService = productService;
    }

    [SwaggerOperation("Returns a list of products available in the bogus catalog.")]
    [HttpGet]
    public async Task<ProductResult.Index> GetIndex([FromQuery] ProductRequest.Index request)
    {
        return await productService.GetIndexAsync(request);
    }

    [SwaggerOperation("Returns a specific product available in the bogus catalog.")]
    [HttpGet("{productId}")]
    public async Task<ProductDto.Detail> GetDetail(int productId)
    {
        return await productService.GetDetailAsync(productId);
    }

    [SwaggerOperation("Creates a new product in the catalog.")]
    [HttpPost]
    public async Task<IActionResult> Create(ProductDto.Mutate model)
    {
        var productId = await productService.CreateAsync(model);
        return CreatedAtAction(nameof(Create), new { id = productId});
    }

    [SwaggerOperation("Edites an existing product in the catalog.")]
    [HttpPut("{productId}")]
    public async Task<IActionResult> Edit(int productId, ProductDto.Mutate model)
    {
        await productService.EditAsync(productId, model);
        return NoContent();
    }

    [SwaggerOperation("Deletes an existing product in the catalog.")]
    [HttpDelete("{productId}")]
    public async Task<IActionResult> Delete(int productId)
    {
        await productService.DeleteAsync(productId);
        return NoContent();
    }

    [SwaggerOperation("Adds an existing tag to an existing product.")]
    [HttpPost("{productId}/tags/{tagId}")]
    public async Task<IActionResult> AddTag(int productId, int tagId)
    {
        await productService.AddTagAsync(productId, tagId);
        return Ok();
    }
}
