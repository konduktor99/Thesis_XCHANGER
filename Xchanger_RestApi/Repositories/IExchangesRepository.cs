using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xchanger_RestApi.DTOs;
using Xchanger_RestApi.Models;

namespace Xchanger_RestApi.Repositories
{
    public interface IExchangesRepository
    {

        public Task<IEnumerable<Exchange>> GetExchangesAsync();
        public Task<IEnumerable<Exchange>> GetRequestedExchangesAsync(int initiatorId);
        public Task<IEnumerable<Exchange>> GetReceivedExchangesAsync(int receiverId);
        public Task<Exchange> GetExchangeAsync(int idExchange);
        public Task<Exchange> RequestExchangeAsync(RequestExchangeDTO exchangeDTO,  int initiatorId);
        public Task<Exchange> ReplyExchangeAsync(int idExchange, ReplyExchangeDTO exchangeDTO);
        public Task<Exchange> AcceptExchangeAsync(int idExchange);
        public Task<Exchange> DeleteExchangeAsync(int idExchange);
    }
}
