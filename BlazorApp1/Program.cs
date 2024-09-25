using BlazorApp1;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using Worker;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped((sv) => {
    Console.WriteLine("WOA");
    return new MGEXWorkerService(sv.GetRequiredService<IJSRuntime>());
});

new MGEXWorkerService(builder.Services.BuildServiceProvider().GetRequiredService<IJSRuntime>());

await builder.Build().RunAsync();
