using Microsoft.AspNetCore.Mvc;
using BogusStore.Shared.Products;
using Swashbuckle.AspNetCore.Annotations;

namespace BogusStore.Server.Controllers.Products;

[ApiController]
[Route("[controller]")]
public class TagController : ControllerBase
{
    private readonly ITagService tagService;

    public TagController(ITagService tagService)
    {
        this.tagService = tagService;
    }

    [SwaggerOperation("Returns a list of all tags available in the bogus catalog.")]
    [HttpGet]
    public async Task<TagResult.Index> GetIndex([FromQuery] TagRequest.Index request)
    {
        return await tagService.GetIndexAsync(request);
    }

    [SwaggerOperation("Creates a new tag in the catalog.")]
    [HttpPost]
    public async Task<IActionResult> Create(TagDto.Mutate model)
    {
        var tagId = await tagService.CreateAsync(model);
        return CreatedAtAction(nameof(Create), new { id = tagId});
    }

    [SwaggerOperation("Edites an existing tag in the catalog.")]
    [HttpPut("{tagId}")]
    public async Task<IActionResult> Edit(int tagId, TagDto.Mutate model)
    {
        await tagService.EditAsync(tagId, model);
        return Ok();

    }
}
