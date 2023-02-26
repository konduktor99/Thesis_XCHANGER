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

        public async Task<IEnumerable<GetExchangeDTO>> GetReceivedExchangesAsync(int receiverId)
        {
            return await _dbContext.Exchanges.Where(e => e.Items.UserId == receiverId && (e.State == 0 || e.State == 1))
                .Select(e => new GetExchangeDTO
                {
                    Id = e.Id,
                    RequestDate = e.RequestDate,
                    ReplyDate = e.ReplyDate,
                    AcceptDate = e.AcceptDate,
                    State = e.State,
                    Mess1 = e.Mess1,
                    Mess2 = e.Mess2,
                    Initiator = new { Id = e.Initiator.Id, Login = e.Initiator.Login },
                    Receiver = new { Id = e.Items.Users.Id, Login = e.Items.Users.Login },
                    Item = new { Id = e.Items.Id, Title = e.Items.Title, IsNew = e.Items.IsNew, Location = e.Items.Location, },
                    Item2 = new { Id = e.Items2.Id, Title = e.Items2.Title, IsNew = e.Items2.IsNew, Location = e.Items2.Location },
                })
                .OrderByDescending(e => e.RequestDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<GetExchangeDTO>> GetRequestedExchangesAsync(int initiatorId)
        {
            return await _dbContext.Exchanges.Where(e => e.Initiator.Id == initiatorId && (e.State == 0 || e.State == 1))
                .Select(e => new GetExchangeDTO { 
                Id = e.Id,
                RequestDate = e.RequestDate,
                ReplyDate = e.ReplyDate,
                AcceptDate = e.AcceptDate,
                State = e.State,
                Mess1 = e.Mess1,
                Mess2 = e.Mess2,
                Initiator = new {Id = e.Initiator.Id, Login = e.Initiator.Login},
                Receiver = new {Id = e.Items.Users.Id, Login = e.Items.Users.Login},
                Item = new {Id = e.Items.Id, Title = e.Items.Title, IsNew = e.Items.IsNew, Location = e.Items.Location, },
                Item2 = new { Id = e.Items2.Id, Title = e.Items2.Title, IsNew = e.Items2.IsNew, Location = e.Items2.Location},
                })
                .OrderByDescending(e => e.RequestDate)
                .ToListAsync();
        }
        public async Task<IEnumerable<GetExchangeDTO>> GetHistoricalExchangesAsync(int userId)
        {
         return await _dbContext.Exchanges
                .Where(e => 
                      (e.Initiator.Id == userId || e.Items.UserId == userId) && (e.State == 2 || e.State == 3)    
                ).Select(e => new GetExchangeDTO
                {
                    Id = e.Id,
                    RequestDate = e.RequestDate,
                    ReplyDate = e.ReplyDate,
                    AcceptDate = e.AcceptDate,
                    State = e.State,
                    Mess1 = e.Mess1,
                    Mess2 = e.Mess2,
                    Initiator = new {Id = e.Initiator.Id,Login = e.Initiator.Login},
                    Receiver = new {Id = e.Items.Users.Id, Login = e.Items.Users.Login},
                    Item = new {
                                Id = e.Items.Id,
                                Title = e.Items.Title,
                                IsNew = e.Items.IsNew,
                                Location = e.Items.Location
                                },
                    Item2 = new {
                                Id = e.Items2.Id,
                                Title = e.Items2.Title,
                                IsNew = e.Items2.IsNew,
                                Location = e.Items2.Location
                                },
                })
                .OrderBy(e => e.AcceptDate)
                .ToListAsync();
        }

        public async Task<Exchange> GetExchangeAsync(int idExchange)
        {
            var exchange = await _dbContext.Exchanges
                .Include(e => e.Items)
                .Include(e => e.Items2)
                .FirstOrDefaultAsync(i => i.Id == idExchange);

            return exchange;
        }
        public async Task<int> GetExchangeReceiverIdAsync(int idExchange)
        {
            var receiverId= await _dbContext.Exchanges.Where(i => i.Id == idExchange).Select(e=>e.Items.UserId).FirstOrDefaultAsync();

            return receiverId;
        }

        public async Task<Exchange> RequestExchangeAsync(RequestExchangeDTO reqExchangeDTO, int initiatorId)
        {
            Exchange exchange = new Exchange();
            exchange.RequestDate = DateTime.Now;

            exchange.Items = await _dbContext.Items.Where(i => i.Id == reqExchangeDTO.ItemId).FirstOrDefaultAsync();
            exchange.ItemId = reqExchangeDTO.ItemId;
            exchange.Mess1 = reqExchangeDTO.Mess1;
            exchange.State = 0;
            exchange.InitiatorId = initiatorId;

            await _dbContext.Exchanges.AddAsync(exchange);
            await _dbContext.SaveChangesAsync();
            return exchange;
        }

        public async Task<Exchange> ReplyExchangeAsync(int idExchange, ReplyExchangeDTO repExchangeDTO)
        {
            var exchange = await GetExchangeAsync(idExchange);

            exchange.ReplyDate = DateTime.Now;
            exchange.State = 1;
            exchange.Items2 = await _dbContext.Items.Where(i => i.Id == repExchangeDTO.Item2Id).FirstOrDefaultAsync();
            exchange.Item2Id = repExchangeDTO.Item2Id;
            exchange.Mess2 = repExchangeDTO.Mess2;

            _dbContext.Entry(exchange).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return exchange;
        }

        public async Task<Exchange> AcceptExchangeAsync(Exchange exchange)
        {

            exchange.AcceptDate = DateTime.Now;
            exchange.Items.Active = false;
            exchange.Items2.Active = false;
            exchange.State = 2;
            
            _dbContext.Entry(exchange).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return exchange;
        }

        public async Task<Exchange> RejectExchangeAsync(Exchange exchange)
        {

            exchange.State = 3;
            _dbContext.Entry(exchange).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return exchange;
        }

        public async Task<Exchange> DeleteExchangeAsync(Exchange exchange)
        {

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

        public async Task<bool> IsExchangeRequestedByUserAsync(int idItem, int userId)
        {
            return await _dbContext.Exchanges.AnyAsync(e => (e.ItemId == idItem && e.InitiatorId == userId && (e.State == 0 || e.State == 1))
            || (e.Item2Id == idItem && e.Items.UserId == userId && (e.State == 0 || e.State == 1)));
        }


    }
}
