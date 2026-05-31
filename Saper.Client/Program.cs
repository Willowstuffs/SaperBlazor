using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Saper.Client;
using Saper.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7139") });
builder.Services.AddScoped<LeaderboardService>(); // Rejestracja serwisu LeaderboardService w kontenerze DI

await builder.Build().RunAsync();
