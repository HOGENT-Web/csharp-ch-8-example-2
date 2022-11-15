using Microsoft.AspNetCore.Components;
using BogusStore.Shared.Products;
using Append.Blazor.Sidepanel;

namespace BogusStore.Client.Products.Components;

public partial class ProductListItem
{
    [Parameter, EditorRequired] public ProductDto.Index Product { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public ISidepanelService Sidepanel { get; set; } = default!;

    private void NavigateToDetail()
    {
        NavigationManager.NavigateTo($"product/{Product.Id}");
    }

    private void ShowEditForm()
    {
        var callback = EventCallback.Factory.Create(this, NavigateToDetail);

        var parameters = new Dictionary<string, object>
        {
            { nameof(Edit.ProductId), Product.Id },
            { nameof(Edit.OnProductEdited), callback  }
        };
        Sidepanel.Open<Edit>("Product", "Wijzigen", parameters);
    }
}
