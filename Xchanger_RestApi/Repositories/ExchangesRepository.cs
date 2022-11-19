using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Exchange>> GetReceivedExchangesAsync()
        {
            return await _dbContext.Exchanges.ToListAsync();
        }

        public async Task<IEnumerable<Exchange>> GetRequestedExchangesAsync()
        {
            return await _dbContext.Exchanges.ToListAsync();
        }

        public async Task<Exchange> GetExchangeAsync(int idExchange)
        {
            var exchange = await _dbContext.Exchanges.FirstOrDefaultAsync(i => i.Id == idExchange);
            return exchange;
        }

        public async Task<Exchange> RequestExchangeAsync(RequestExchangeDTO reqExchangeDTO)
        {
            Exchange exchange = new Exchange();
            exchange.RequestDate = DateTime.Now;
            exchange.AcceptDate = null;
            exchange.Items =await  _dbContext.Items.Where(i => i.Id ==reqExchangeDTO.ItemId).FirstOrDefaultAsync();
            exchange.ItemId = reqExchangeDTO.ItemId;
            exchange.State = 0;
          
            await _dbContext.Exchanges.AddAsync(exchange);
            await _dbContext.SaveChangesAsync();
            return exchange;
        }

        public async Task<Exchange> ReplyExchangeAsync(int idExchange, ReplyExchangeDTO repExchangeDTO)
        {
            
            var exchange = await GetExchangeAsync(idExchange);
           
            exchange.Items2 = await _dbContext.Items.Where(i => i.Id == repExchangeDTO.Item2Id).FirstOrDefaultAsync();
            exchange.Item2Id = repExchangeDTO.Item2Id;

            _dbContext.Entry(exchange).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return exchange;
        }

        public async Task<Exchange> AcceptExchangeAsync(int idExchange)
        {

            var exchange = await GetExchangeAsync(idExchange);

            exchange.State = 1;

            _dbContext.Entry(exchange).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return exchange;
        }

        public async Task<Exchange> DeleteExchange(int idExchange)
        {
            var exchange = await GetExchangeAsync(idExchange);

            if (exchange != null)
            {
                _dbContext.Entry(exchange).State = EntityState.Deleted;
                await _dbContext.SaveChangesAsync();
            }

            return exchange;
        }

    }
}
