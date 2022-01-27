using Application.Commands.TechnologyCategory;
using Application.Queries.TechnologyCategory;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client.HttpRepository.Categories
{
    public class TechnologyCategoriesHttpRepository : BaseHttpRepository, ITechnologyCategoriesHttpRepository
    {
        private const string Url = "TechnologyCategories";

        public TechnologyCategoriesHttpRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory, Url)
        {
        }

        public async Task<List<GetTechnologyCategoriesQuery>> GetAllCategoriesAsync()
        {
            var response = await HttpClient.GetAsync(Url);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var technologies = JsonSerializer.Deserialize<List<GetTechnologyCategoriesQuery>>(content, Options);

            return technologies;
        }

        public async Task<int> CreateTechnologyCategoryAsync(CreateTechnologyCategory categoryToAdd)
        {
            var id = await CreateAsync(categoryToAdd);
            return id;
        }

        public async Task<bool> UpdateTechnologyCategoryAsync(UpdateTechnologyCategory categoryToUpdate)
        {
            var success = await UpdateAsync(categoryToUpdate);
            return success;
        }

        public async Task<bool> DeleteTechnologyCategoryAsync(int id)
        {
            var success = await DeleteAsync(id);
            return success;
        }
    }
}