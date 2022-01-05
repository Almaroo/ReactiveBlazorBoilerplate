using System.Reactive.Concurrency;
using BlazorAppBoilerplate.ApiClients;
using BlazorAppBoilerplate.Features.Pages.Foo;
using BlazorAppBoilerplate.Services;
using ReactiveUI;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

RxApp.MainThreadScheduler = WasmScheduler.Default;
RxApp.TaskpoolScheduler = WasmScheduler.Default;
        
builder.Services.AddOptions();
// Load options
// services.Configure<XYZSettings>(Configuration.GetSection(nameof(XYZSettings)));

builder.Services.AddRazorPages(options =>
{
    options.RootDirectory = @"/Features/Pages";
});
        
builder.Services.AddServerSideBlazor();
        
builder.Services
    .AddRefitClient<IAgeOfEmpiresApiClient>()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new Uri("https://age-of-empires-2-api.herokuapp.com/api/v1");
    });

builder.Services.AddTransient<FooViewModel>();
builder.Services.AddTransient<IAgeOfEmpiresService, AgeOfEmpiresService>();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();