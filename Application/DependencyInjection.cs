using Application.Entities;
using Application.Persistence;
using Application.Repositories;
using Application.Repositories.EmployeeRepository;
using Application.Repositories.TechnologyCategoryRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddTransient<IRepository<Technology>, TechnologyRepository>();
            services.AddTransient<IRepository<TechnologyCategory>, TechnologyCategoryRepository>();
            services.AddTransient<IRepository<Employee>, EmployeeRepository>();
            services.AddTransient<IRepository<Team>, TeamRepository>();
            services.AddTransient<IRepository<Project>, ProjectRepository>();

            return services;
        }
    }
}