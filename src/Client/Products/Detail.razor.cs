﻿using Microsoft.AspNetCore.Components;
using BogusStore.Shared.Products;
using System.Threading.Tasks;


namespace BogusStore.Client.Products;

public partial class Detail
{
    private ProductDto.Detail? product;
    [Parameter] public int Id { get; set; }
    [Inject] public IProductService ProductService { get; set; } = default!;

    protected override async Task OnParametersSetAsync()
    {
        var response = await ProductService.GetDetailAsync(Id);
        product = response;
    }
}
