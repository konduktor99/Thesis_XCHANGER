using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xchanger_RestApi.Models;
using Xchanger_RestApi.DTOs;
using Xchanger_RestApi.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Xchanger_RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ExchangesController : ControllerBase
    {

        private readonly IExchangesRepository _repository;

        public ExchangesController(IExchangesRepository repository)
        {
            _repository = repository;
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
        public async Task<IActionResult> GetRequestedExchanges([FromQuery] int initiatorId)
        {
            try
            {
                var exchanges = await _repository.GetRequestedExchangesAsync(initiatorId);

                if (exchanges.Count() > 0)
                    return Ok(exchanges);
                else
                    return NotFound("Nie znaleziono wysłanych propozycji wymiany");

            }
            catch (Exception)
            {
                return StatusCode(500, "Wystąpił błąd wewnętrzny serwera");

            }


        }

        [HttpGet("Received")]
        public async Task<IActionResult> GetReceivedExchanges([FromQuery] int receiverId)
        {
            try
            {
                var exchanges = await _repository.GetReceivedExchangesAsync(receiverId);

                if (exchanges.Count() > 0)
                    return Ok(exchanges);
                else
                    return NotFound("Nie znaleziono otrzymanych propozycji wymiany");

            }
            catch (Exception)
            {
                return StatusCode(500, "Wystąpił błąd wewnętrzny serwera");

            }


        }

        [HttpGet("{idExchange}")]
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
        public async Task<IActionResult> RequestExchange([FromBody] RequestExchangeDTO exchangeDTO, [FromQuery] int initiatorId)
        {
            try
            {
                var exchange = await _repository.RequestExchangeAsync(exchangeDTO, initiatorId);
              

                    return Ok(exchange);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Wystąpił błąd wewnętrzny serwera \n" + ex);
            }

        }

        [HttpPut("ReplyExchangeRequest/{idExchange}")]
        public async Task<IActionResult> ReplyExchange([FromBody] ReplyExchangeDTO exchangeDTO, [FromRoute] int idExchange)
        {
            try
            {
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
        public async Task<IActionResult> AcceptExchange([FromRoute] int idExchange)
        {
            try
            {
                var exchange = await _repository.AcceptExchangeAsync(idExchange);

                if (exchange == null)
                    return NotFound("Nie znaleziono przedmiotu");

                return Ok(exchange);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Wystąpił błąd wewnętrzny serwera \n" + ex);
            }

        }

        [HttpDelete("DeleteExchange/{idExchange}")]
        public async Task<IActionResult> DeleteItem([FromRoute] int idExchange)
        {
            try
            {
                var item = await _repository.DeleteExchangeAsync(idExchange);

                if (item == null)
                    return NotFound("Nie znaleziono przedmiotu");

                return Ok(item);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Wystąpił błąd wewnętrzny serwera \n" + ex);
            }

        }


    }


}
