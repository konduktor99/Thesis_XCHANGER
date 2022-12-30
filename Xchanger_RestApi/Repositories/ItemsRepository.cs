using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xchanger_RestApi.DTOs;
using Xchanger_RestApi.Models;

namespace Xchanger_RestApi.Repositories
{
    public class ItemsRepository : IItemsRepository
    {

        private XchangerDbContext _dbContext;

        public ItemsRepository(XchangerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await _dbContext.Items.ToListAsync();
        }

        public async Task<IEnumerable<GetItemsDTO>> GetActiveItemsAsync(string? category, string? user)
        {
            IQueryable<Item> items;
            if ((category == null || category == "") && (user == null || user == ""))
                 items = _dbContext.Items.Where(i => i.Active == true);

            else if (category == null || category == "")
                items = _dbContext.Items.Where(i => i.Active == true && i.Users.Login == user);

            else if (user == null || user == "")
                items = _dbContext.Items.Where(i => i.Active == true && i.Categories.Name == category);

            else
             items = _dbContext.Items.Where(i => i.Active == true && i.Users.Login == user && i.Categories.Name == category);

            return await items.Select(i => new GetItemsDTO
            {

                Id = i.Id,
                Title = i.Title,
                Description = i.Description,
                Active = i.Active,
                PublicationDate = i.PublicationDate,
                IsNew = i.IsNew,
                Location = i.Location,
                CategoryId = i.CategoryId
            }).ToListAsync();

        }

        public async Task<GetItemDTO> GetItemDtoAsync(int idItem)
        {
            var item = await _dbContext.Items.Where(i => i.Id == idItem).Select(i => new GetItemDTO{

                Id = i.Id,
                Title = i.Title,
                Description = i.Description,
                Active = i.Active,
                PublicationDate = i.PublicationDate,
                IsNew = i.IsNew,
                Location = i.Location,
                Category = new { Id = i.Categories.Id, Name = i.Categories.Name },
                User = new { Id = i.Users.Id, Login = i.Users.Login, PhoneNumber = i.Users.PhoneNumber }
            })
            .FirstOrDefaultAsync();

          

            return item;
        }

        public async Task<Item> GetItemAsync(int idItem)
        {
            var item = await _dbContext.Items.FirstOrDefaultAsync(i => i.Id == idItem);
            return item;
        }



        public async Task<Item> CreateItemAsync(CreateItemDTO itemDTO, int userId)
        {
            Item item = new Item();
            item.Active = true;
            item.Title = itemDTO.Title;
            item.Description = itemDTO.Description;
            item.UserId = userId;
            item.CategoryId = itemDTO.CategoryId;
            item.Location = itemDTO.Location;
            item.IsNew = itemDTO.IsNew;
            item.PublicationDate = DateTime.Today;

            _dbContext.Entry(item).State = EntityState.Modified;
           
            await _dbContext.Items.AddAsync(item);
            await _dbContext.SaveChangesAsync();


            var user2 = await _dbContext.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();

            return item;
        }

        public async Task<Item> UpdateItemAsync(Item item, CreateItemDTO itemDTO)
        {
            

            if (item != null)
            {
                item.Title = itemDTO.Title;
                item.Description = itemDTO.Description;
                item.CategoryId = itemDTO.CategoryId;
                item.PublicationDate = DateTime.Today;
                item.IsNew = itemDTO.IsNew;
                item.Location = itemDTO.Location;


                _dbContext.Entry(item).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
          
            return item;
        }


        public async Task<Item> DeleteItem(int idItem)
        {
            var item = await _dbContext.Items
            .Include(b => b.ExchangeItems)
            .Include(b => b.ExchangeItems2)
            .FirstOrDefaultAsync(i => i.Id == idItem);



            if (item != null)
            {
                if (item.Active)
                {


                    _dbContext.Items.Remove(item);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception();
                }

        }
               

            return item;
        }


    }
}
