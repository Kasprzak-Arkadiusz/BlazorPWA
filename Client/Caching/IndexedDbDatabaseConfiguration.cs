using DnetIndexedDb;
using DnetIndexedDb.Fluent;
using DnetIndexedDb.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Client.Caching
{
    public static class IndexedDbDatabaseConfiguration
    {
        public static IServiceCollection ConfigureIndexedDbDatabase(this IServiceCollection services)
        {
            services.AddIndexedDbDatabase<GridColumnDataIndexedDb>(options =>
            {
                var model = new IndexedDbDatabaseModel()
                    .WithName("OfflineCache")
                    .WithVersion(1)
                    .WithModelId(0);
                
                model.AddStore("EmployeesQuery")
                    .WithAutoIncrementingKey("Id")
                    .AddUniqueIndex("Id")
                    .AddIndex("FirstName")
                    .AddIndex("LastName")
                    .AddIndex("Age")
                    .AddIndex("TechnologyNames")
                    .AddIndex("TeamId");

                model.AddStore("ProjectsQuery")
                    .WithAutoIncrementingKey("Id")
                    .AddUniqueIndex("Id")
                    .AddIndex("Name")
                    .AddIndex("StartDate")
                    .AddIndex("TeamId")
                    .AddIndex("TechnologyNames");

                model.AddStore("TeamsQuery")
                    .WithAutoIncrementingKey("Id")
                    .AddUniqueIndex("Id")
                    .AddIndex("ProjectName");

                model.AddStore("TechnologiesQuery")
                    .WithAutoIncrementingKey("Id")
                    .AddUniqueIndex("Id")
                    .AddIndex("Name")
                    .AddIndex("CategoryName");

                model.AddStore("CategoriesQuery")
                    .WithAutoIncrementingKey("Id")
                    .AddUniqueIndex("Id")
                    .AddIndex("Name");

                options.UseDatabase(model);
            });

            return services;
        }
    }
}
