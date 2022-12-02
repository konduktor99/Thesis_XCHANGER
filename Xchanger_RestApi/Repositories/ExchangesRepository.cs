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

        public async Task<IEnumerable<Exchange>> GetReceivedExchangesAsync(int receiverId)
        {
            return await _dbContext.Exchanges.Where(e => e.Items.UserId == receiverId && e.State == 0).ToListAsync();
        }

        public async Task<IEnumerable<Exchange>> GetRequestedExchangesAsync(int initiatorId)
        {
            return await _dbContext.Exchanges.Where(e => e.Initiator.Id == initiatorId && e.State == 0).ToListAsync();
        }

        public async Task<Exchange> GetExchangeAsync(int idExchange)
        {
            var exchange = await _dbContext.Exchanges.FirstOrDefaultAsync(i => i.Id == idExchange);

            return exchange;
        }

        public async Task<Exchange> RequestExchangeAsync(RequestExchangeDTO reqExchangeDTO, int initiatorId)
        {
            Exchange exchange = new Exchange();
            exchange.RequestDate = DateTime.Now;
           
            exchange.Items =await  _dbContext.Items.Where(i => i.Id ==reqExchangeDTO.ItemId).FirstOrDefaultAsync();
            exchange.ItemId = reqExchangeDTO.ItemId;
            exchange.State = 0;
            exchange.InitiatorId = initiatorId;
          
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
            exchange.Items.Active = false;
            exchange.Items2.Active = false;
            _dbContext.Entry(exchange).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return exchange;
        }

        public async Task<Exchange> DeleteExchangeAsync(int idExchange)
        {
            var exchange = await GetExchangeAsync(idExchange);

            if (exchange != null)
            {
                _dbContext.Entry(exchange).State = EntityState.Deleted;
                await _dbContext.SaveChangesAsync();
            }

            return exchange;
        }

        public async Task<IEnumerable<Exchange>> GetExchangesAsync()
        {
            return await _dbContext.Exchanges.ToListAsync();
        }




    }
}
