using System.Collections.Generic;
using System.Threading.Tasks;
using Xchanger_RestApi.DTOs;
using Xchanger_RestApi.Models;

namespace Xchanger_RestApi.Repositories
{
    public interface IItemsRepository
    {

        public Task<IEnumerable<Item>> GetItemsAsync();
        public Task<IEnumerable<Item>> GetActiveItemsAsync(string? category, string? user);
        public Task<dynamic> GetItemDtoAsync(int idItem);
        public Task<Item> GetItemAsync(int idItem);
        public Task<Item> CreateItemAsync(ItemDTO itemDTO);
        public Task<Item> UpdateItemAsync(int idItem, ItemDTO itemDTO);
        public Task<Item> DeleteItem(int IdItem);
    }
}
