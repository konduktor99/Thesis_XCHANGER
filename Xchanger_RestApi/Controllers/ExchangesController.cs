using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xchanger_RestApi.DTOs;
using Xchanger_RestApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Xchanger_RestApi.Helpers;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Xchanger_RestApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    
    public class ExchangesController : ControllerBase
    {

        private readonly IExchangesRepository _repository;
        public static IWebHostEnvironment _env;

        public ExchangesController(IExchangesRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetExchanges()
        {
            try
            {
                var exchanges = await _repository.GetExchangesAsync();

                if (exchanges.Count() > 0)
                    return Ok(exchanges);
                else
                    return NotFound("Nie znaleziono transakcji wymiany");

            }
            catch(Exception)
            {
                return StatusCode(500, "Wystąpił błąd wewnętrzny serwera");
               
            }

            
        }
        [HttpGet("Requested")]
        [Authorize]
        public async Task<IActionResult> GetRequestedExchanges()
        {
            try
            {
                var initiatorId = JwtDecoder.GetClaimFromJwt(Request.Headers["authorization"].ToString().Substring(7), "id");

                var exchanges = await _repository.GetRequestedExchangesAsync((int)initiatorId);

                if (exchanges.Count() > 0)
                    return Ok(exchanges.Select(e => {
                        e.ImgBytes1 = LoadMainImage(e.Item.Id);
                        e.ImgBytes2 = LoadMainImage(e.Item2.Id);
                        return e; 
                    }));
                else
                    return NotFound("Nie znaleziono wysłanych propozycji wymiany");

            }
            catch (Exception)
            {
                return StatusCode(500, "Wystąpił błąd wewnętrzny serwera");

            }


        }

        [HttpGet("Received")]
        [Authorize]
        public async Task<IActionResult> GetReceivedExchanges()
        {
            try
            {
                var receiverId = JwtDecoder.GetClaimFromJwt(Request.Headers["authorization"].ToString().Substring(7), "id");
                var exchanges = await _repository.GetReceivedExchangesAsync((int)receiverId);

                if (exchanges.Count() > 0)
                    return Ok(exchanges.Select(e => {
                        e.ImgBytes1 = LoadMainImage(e.Item.Id);
                        e.ImgBytes2 = LoadMainImage(e.Item2.Id);
                        return e;
                    }));
                else
                    return NotFound("Nie znaleziono otrzymanych propozycji wymiany");

            }
            catch (Exception)
            {
                return StatusCode(500, "Wystąpił błąd wewnętrzny serwera");

            }


        }
        [HttpGet("History")]
        [Authorize]
        public async Task<IActionResult> GetHistoricalExchanges()
        {
            try
            {
                var userId = JwtDecoder.GetClaimFromJwt(Request.Headers["authorization"].ToString().Substring(7), "id");

                var exchanges = await _repository.GetHistoricalExchangesAsync((int)userId);

                if (exchanges.Count() > 0)
                    return Ok(exchanges.Select(e => {
                        e.ImgBytes1 = LoadMainImage(e.Item.Id);
                        e.ImgBytes2 = LoadMainImage(e.Item2.Id);
                        return e;
                    }));
                else
                    return NotFound("Brak historii wymian");

            }
            catch (Exception)
            {
                return StatusCode(500, "Wystąpił błąd wewnętrzny serwera");

            }


        }

        [HttpGet("IsRequested/{idItem}")]
        public async Task<IActionResult> IsExchangeRequestedByUser([FromRoute] int idItem)
        {
            try
            {
                var userId = JwtDecoder.GetClaimFromJwt(Request.Headers["authorization"].ToString().Substring(7), "id");

                var isRequested = await _repository.IsExchangeRequestedByUserAsync(idItem, (int)userId);

                return Ok(isRequested);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Wystąpił błąd wewnętrzny serwera"+ ex);

            }


        }

        [HttpGet("{idExchange}")]
        [Authorize]
        public async Task<IActionResult> GetExchange([FromRoute] int idExchange)
        {
            try
            {
                var exchange =await _repository.GetExchangeAsync(idExchange);

                if (exchange != null)
                    return Ok(exchange);
                else
                    return NotFound("Nie znaleziono takiej transakcji");

            }
            catch (Exception)
            {
                return StatusCode(500, "Wystąpił błąd wewnętrzny serwera");

            }

        }

        [HttpPost("RequestExchange")]
        [Authorize]
        public async Task<IActionResult> RequestExchange([FromBody] RequestExchangeDTO exchangeDTO)
        {
            try
            {

                var initiatorId = JwtDecoder.GetClaimFromJwt(Request.Headers["authorization"].ToString().Substring(7),"id");
                var exchange = await _repository.RequestExchangeAsync(exchangeDTO, (int)initiatorId);

                    return Ok(exchange);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Wystąpił błąd wewnętrzny serwera \n" + ex);
            }

        }

        [HttpPut("ReplyExchangeRequest/{idExchange}")]
        [Authorize]
        public async Task<IActionResult> ReplyExchange([FromBody] ReplyExchangeDTO exchangeDTO, [FromRoute] int idExchange)
        {
            try
            {
                var userId = JwtDecoder.GetClaimFromJwt(Request.Headers["authorization"].ToString().Substring(7), "id");
                if (userId == null)
                    return Unauthorized();

                var exchangeReceiverId = await _repository.GetExchangeReceiverIdAsync(idExchange);
                if (userId != exchangeReceiverId)
                    return Unauthorized("Nie można ingerować w nieswoją transakcję wymiany");

                var exchange = await _repository.ReplyExchangeAsync(idExchange, exchangeDTO);

                if (exchange == null)
                    return NotFound("Nie znaleziono przedmiotu");

                return Ok(exchange);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Wystąpił błąd wewnętrzny serwera \n" + ex);
            }

        }

        [HttpPut("AcceptExchange/{idExchange}")]
        [Authorize]
        public async Task<IActionResult> AcceptExchange([FromRoute] int idExchange)
        {
            try
            {
                var userId = JwtDecoder.GetClaimFromJwt(Request.Headers["authorization"].ToString().Substring(7), "id");
                if (userId == null)
                    return Unauthorized();

                var exchange = await _repository.GetExchangeAsync(idExchange);
                if (userId != exchange.InitiatorId)
                    return Unauthorized("Nie można ingerować w nieswoją transakcję wymiany");

                if (exchange == null)
                    return NotFound("Nie znaleziono przedmiotu");

                var acceptedExchange = await _repository.AcceptExchangeAsync(exchange);

                return Ok(acceptedExchange);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Wystąpił błąd wewnętrzny serwera \n" + ex);
            }

        }

        [HttpPut("RejectExchange/{idExchange}")]
        [Authorize]
        public async Task<IActionResult> RejectExchange([FromRoute] int idExchange)
        {
            try
            {
                var userId = JwtDecoder.GetClaimFromJwt(Request.Headers["authorization"].ToString().Substring(7), "id");

                var exchange = await _repository.GetExchangeAsync(idExchange);
                if (userId != exchange.InitiatorId && userId != await _repository.GetExchangeReceiverIdAsync(idExchange))
                    return Unauthorized("Nie można ingerować w nieswoją transakcję wymiany");

                if (exchange == null)
                    return NotFound("Nie znaleziono przedmiotu");

                var rejectedExchange = await _repository.RejectExchangeAsync(exchange);



                return Ok(rejectedExchange);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Wystąpił błąd wewnętrzny serwera \n" + ex);
            }

        }

        [HttpDelete("DeleteExchange/{idExchange}")]
        [Authorize]
        public async Task<IActionResult> DeleteExchange([FromRoute] int idExchange)
        {
            try
            {
                var userId = JwtDecoder.GetClaimFromJwt(Request.Headers["authorization"].ToString().Substring(7), "id");

                var exchange = await _repository.GetExchangeAsync(idExchange);
                if (userId != exchange.InitiatorId && userId != exchange.Items.UserId)
                    return Unauthorized("Nie można ingerować w nieswoją transakcję wymiany");

                if (exchange == null)
                    return NotFound("Nie znaleziono przedmiotu");

                var deletedExchange = await _repository.DeleteExchangeAsync(exchange);

                if (exchange == null)
                    return NotFound("Nie znaleziono przedmiotu");

                return Ok(deletedExchange);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Wystąpił błąd wewnętrzny serwera \n" + ex);
            }

        }

        public static byte[] LoadMainImage(int itemId)
        {

            byte[] imgBytes = null;
            var itemFolderPath = Path.Combine(_env.WebRootPath, "itemPics", itemId.ToString());

            string file;
            if (Directory.Exists(itemFolderPath))
            {
                file = Directory.EnumerateFiles(itemFolderPath, "*.jpg").FirstOrDefault();
                if (file != null)
                    imgBytes = System.IO.File.ReadAllBytes(file);
            }


            return imgBytes;
        }


    }


}
