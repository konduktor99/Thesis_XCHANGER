﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xchanger_RestApi.DTOs;
using Xchanger_RestApi.Models;

namespace Xchanger_RestApi.Repositories
{
    public class ExchangesRepository : IExchangesRepository
    {

        private XchangerDbContext _dbContext;

        public ExchangesRepository(XchangerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await _dbContext.Items.ToListAsync();
        }

        public async Task<Item> GetItemAsync(int idItem)
        {
            var item = await _dbContext.Items.FirstOrDefaultAsync(i => i.Id == idItem);
            return item;
        }


        public async Task<Item> CreateItemAsync(ItemDTO itemDTO)
        {
            Item item = new Item();
            item.Title = itemDTO.Title;
            item.Description = itemDTO.Description;
           // item.Categories = await _dbContext.Categories.Where(c => c.Id == itemDTO.CategoryId).FirstOrDefaultAsync();
           // item.Users = await _dbContext.Users.Where(c => c.Id == itemDTO.UserId).FirstOrDefaultAsync();
            item.UsersId = itemDTO.UserId;  //itemDTO.CategoryId;
            item.CategoriesId = itemDTO.CategoryId;
            item.PublicationDate = DateTime.Today;


           
            await _dbContext.Items.AddAsync(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }

        public async Task<Item> UpdateItemAsync(int idItem, ItemDTO itemDTO)
        {
            
            var item = await GetItemAsync(idItem);

          
            item.Title = itemDTO.Title;
            item.Description = itemDTO.Description;
            // item.Categories = await _dbContext.Categories.Where(c => c.Id == itemDTO.CategoryId).FirstOrDefaultAsync();
            // item.Users = await _dbContext.Users.Where(c => c.Id == itemDTO.UserId).FirstOrDefaultAsync();
            //item.UsersId = itemDTO.UserId;  //itemDTO.CategoryId;
            item.CategoriesId = itemDTO.CategoryId;
            item.PublicationDate = DateTime.Today;


            _dbContext.Entry(item).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return item;
        }

    }
}
