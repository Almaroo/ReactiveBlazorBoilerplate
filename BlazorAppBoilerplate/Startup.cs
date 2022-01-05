using System;
using System.Reactive.Concurrency;
using BlazorAppBoilerplate.ApiClients;
using BlazorAppBoilerplate.Features.Pages.Foo;
using BlazorAppBoilerplate.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReactiveUI;
using Refit;

namespace BlazorAppBoilerplate;
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        RxApp.MainThreadScheduler = WasmScheduler.Default;
        RxApp.TaskpoolScheduler = WasmScheduler.Default;
        
        services.AddOptions();
        LoadOptions(services);

        services.AddRazorPages(options =>
        {
            options.RootDirectory = @"/Features/Pages";
        });
        
        services.AddServerSideBlazor();
        
        services
            .AddRefitClient<IAgeOfEmpiresApiClient>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri("https://age-of-empires-2-api.herokuapp.com/api/v1");
            });

        services.AddTransient<FooViewModel>();
        services.AddTransient<IAgeOfEmpiresService, AgeOfEmpiresService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapFallbackToPage("/_Host");
        });
    }
    
    private void LoadOptions(IServiceCollection services)
    {
        // services.Configure<XYZSettings>(Configuration.GetSection(nameof(XYZSettings)));
    }
}