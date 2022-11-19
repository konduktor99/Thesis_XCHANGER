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

        [HttpPost("CreateExchange")]
        public async Task<IActionResult> CreateItem([FromBody] RequestExchangeDTO exchangeDTO)
        {
            try
            {
                var exchange = await _repository.CreateExchangeAsync(exchangeDTO);
              

                    return Ok(exchange);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Wystąpił błąd wewnętrzny serwera \n" + ex);
            }

        }

        [HttpPut("{idExchange}")]
        public async Task<IActionResult> UpdateItem([FromBody] ItemDTO exchangeDTO, [FromRoute] int idExchange)
        {
            try
            {
                var exchange = await _repository.UpdateExchangeAsync(idExchange, exchangeDTO);

                if (exchange == null)
                    return NotFound("Nie znaleziono przedmiotu");

                return Ok(exchange);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Wystąpił błąd wewnętrzny serwera \n" + ex);
            }

        }

    }


}
