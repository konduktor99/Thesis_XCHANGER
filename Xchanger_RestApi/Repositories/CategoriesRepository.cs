using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xchanger_RestApi.DTOs;
using Xchanger_RestApi.Models;

namespace Xchanger_RestApi.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {

        private XchangerDbContext _dbContext;

        public CategoriesRepository(XchangerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Exchange>> GetReceivedExchangesAsync(int receiverId)
        {
            return await _dbContext.Exchanges.Where(e => e.Items.UserId == receiverId && e.State == 0).ToListAsync();
        }
        public async Task<IEnumerable<GetCategoryDTO>> GetCategoriesAsync()
        {
            return await _dbContext.Categories.Select(c => new GetCategoryDTO {Id = c.Id, Name = c.Name }).ToListAsync();
        }


    }
}
