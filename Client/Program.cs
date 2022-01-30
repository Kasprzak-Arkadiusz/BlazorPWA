using Client.Caching;
using Client.HttpRepository.Categories;
using Client.HttpRepository.Employees;
using Client.HttpRepository.Projects;
using Client.HttpRepository.Teams;
using Client.HttpRepository.Technologies;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddSyncfusionBlazor();

            builder.Services.AddHttpClient("WebAPI", client =>
                client.BaseAddress = new Uri("https://localhost:5011/"));

            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("WebAPI"));

            builder.Services.AddTransient<IEmployeesHttpRepository, EmployeesHttpRepository>();
            builder.Services.AddTransient<ITechnologiesHttpRepository, TechnologiesHttpRepository>();
            builder.Services.AddTransient<ITechnologyCategoriesHttpRepository, TechnologyCategoriesHttpRepository>();
            builder.Services.AddTransient<ITeamsHttpRepository, TeamsHttpRepository>();
            builder.Services.AddTransient<IProjectsHttpRepository, ProjectsHttpRepository>();

            builder.Services.ConfigureIndexedDbDatabase();

            await builder.Build().RunAsync();
        }
    }
}