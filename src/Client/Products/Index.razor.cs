using Microsoft.AspNetCore.Components;
using BogusStore.Shared.Products;
using System.Collections.Generic;
using System.Threading.Tasks;
using Append.Blazor.Sidepanel;

namespace BogusStore.Client.Products;

public partial class Index
{
    [Inject] public IProductService ProductService { get; set; } = default!;
    [Inject] public ISidepanelService Sidepanel { get; set; } = default!;

    private IEnumerable<ProductDto.Index>? products;

    protected override async Task OnInitializedAsync()
    {
        ProductRequest.Index request = new();
        var response = await ProductService.GetIndexAsync(request);
        products = response.Products;
    }

    private void ShowCreateForm()
    {
        Sidepanel.Open<Components.Create>("Product", "Toevoegen");
    }
}
