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

    private List<ProductDto.Index>? products = new();

    protected override async Task OnInitializedAsync()
    {
        var request = new ProductRequest.Index();
        var response = await ProductService.GetIndexAsync(request);
        products = response.Products!.ToList();
    }

    private void ShowCreateForm()
    {
        Sidepanel.Open<Components.Create>("Product", "Toevoegen");
    }

    private void DeleteAsync(ProductDto.Index product)
    {
        products!.Remove(product);
    }
}
