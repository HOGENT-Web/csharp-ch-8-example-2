using Microsoft.AspNetCore.Components;
using BogusStore.Shared.Products;
using System.Threading.Tasks;


namespace BogusStore.Client.Products;

public partial class Detail
{
    private ProductDto.Detail? product;
    private bool isRequestingDelete;

    [Parameter] public int Id { get; set; }
    [Inject] public IProductService ProductService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    protected override async Task OnParametersSetAsync()
    {
        var response = await ProductService.GetDetailAsync(Id);
        product = response;
    }

    private void RequestDelete()
    {
        isRequestingDelete = true;
    }

    private void CancelDeleteRequest()
    {
        isRequestingDelete = false;
    }

    private async Task DeleteProductAsync()
    {
        await ProductService.DeleteAsync(Id);
        NavigationManager.NavigateTo("product");
    }
}
