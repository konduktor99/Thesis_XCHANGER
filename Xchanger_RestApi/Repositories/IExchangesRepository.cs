using System.Collections.Generic;
using System.Threading.Tasks;
using Xchanger_RestApi.DTOs;
using Xchanger_RestApi.Models;

namespace Xchanger_RestApi.Repositories
{
    public interface IExchangesRepository
    {

        public Task<IEnumerable<Exchange>> GetExchangesAsync();
        public Task<IEnumerable<GetExchangeDTO>> GetRequestedExchangesAsync(int initiatorId);
        public Task<IEnumerable<GetExchangeDTO>> GetReceivedExchangesAsync(int receiverId);
        public Task<IEnumerable<GetExchangeDTO>> GetHistoricalExchangesAsync(int userId);
        public Task<Exchange> GetExchangeAsync(int idExchange);
        public Task<Exchange> RequestExchangeAsync(RequestExchangeDTO exchangeDTO,  int initiatorId);
        public Task<Exchange> ReplyExchangeAsync(int idExchange, ReplyExchangeDTO exchangeDTO);
        public Task<Exchange> AcceptExchangeAsync(Exchange exchange);
        public Task<Exchange> DeleteExchangeAsync(Exchange exchange);
        public Task<bool> IsExchangeRequestedByUserAsync(int idItem, int userId);
        public Task<Exchange> RejectExchangeAsync(Exchange exchange);
        public Task<int> GetExchangeReceiverIdAsync(int idExchange);
    }
}
