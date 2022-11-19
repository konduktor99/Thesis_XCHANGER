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
        public Task<Exchange> GetExchangeAsync(int idExchange);
        public Task<Exchange> RequestExchangeAsync(RequestExchangeDTO exchangeDTO);
        public Task<Exchange> ReplyExchangeAsync(int idExchange, ReplyExchangeDTO exchangeDTO);
        public Task<Exchange> AcceptExchangeAsync(int idExchange);
    }
}
