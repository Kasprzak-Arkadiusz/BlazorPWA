using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Infrastructure.Persistence;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddTransient<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddTransient<ITechnologyRepository, TechnologyRepository>();
            services.AddTransient<ITechnologyCategoryRepository, TechnologyCategoryRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<ITeamRepository, TeamRepository>();
            services.AddTransient<IProjectRepository, ProjectRepository>();

            services.AddTransient<ITechnologyService, TechnologyService>();
            services.AddTransient<ITechnologyCategoryService, TechnologyCategoryService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<ITeamService, TeamService>();
            services.AddTransient<IProjectService, ProjectService>();

            return services;
        }
    }
}