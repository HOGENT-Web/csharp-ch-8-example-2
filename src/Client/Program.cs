using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BogusStore.Client;
using Microsoft.AspNetCore.Components.Authorization;
using BogusStore.Client.Authentication;
using BogusStore.Shared.Products;
using BogusStore.Client.Products;
using BogusStore.Client.Infrastructure;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAuthorizationCore();
builder.Services.AddSingleton<FakeAuthenticationProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<FakeAuthenticationProvider>());
builder.Services.AddTransient<FakeAuthorizationMessageHandler>();
builder.Services.AddTransient<CleanErrorHandler>();

builder.Services.AddHttpClient("Project.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<FakeAuthorizationMessageHandler>()
                .AddHttpMessageHandler<CleanErrorHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Project.ServerAPI"));

builder.Services.AddScoped<IProductService, ProductService>();


await builder.Build().RunAsync();
