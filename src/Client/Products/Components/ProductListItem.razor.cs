using Microsoft.AspNetCore.Components;
using BogusStore.Shared.Products;

namespace BogusStore.Client.Products.Components;

public partial class ProductListItem
{
    [Parameter, EditorRequired] public ProductDto.Index Product { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    private void NavigateToDetail()
    {
        NavigationManager.NavigateTo($"product/{Product.Id}");
    }
}
