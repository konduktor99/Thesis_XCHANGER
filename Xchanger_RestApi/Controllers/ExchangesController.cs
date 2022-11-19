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
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var items = await _repository.GetItemsAsync();

                if (items.Count() > 0)
                    return Ok(items);
                else
                    return NotFound("Nie znaleziono przedmiotów");

            }
            catch(Exception)
            {
                return StatusCode(500, "Wystąpił błąd wewnętrzny serwera");
               
            }

            
        }

        [HttpGet("{idItem}")]
        public async Task<IActionResult> GetPrescription([FromRoute] int idItem)
        {
            try
            {
                var item =await _repository.GetItemAsync(idItem);

                if (item != null)
                    return Ok(item);
                else
                    return NotFound("Nie znaleziono przedmiotu");

            }
            catch (Exception)
            {
                return StatusCode(500, "Wystąpił błąd wewnętrzny serwera");

            }

        }

        [HttpPost("CreateItem")]
        public async Task<IActionResult> CreateItem([FromBody] ItemDTO itemDTO)
        {
            try
            {
                var item = await _repository.CreateItemAsync(itemDTO);
              

                    return Ok(item);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Wystąpił błąd wewnętrzny serwera \n" + ex);
            }

        }

        [HttpPut("{idItem}")]
        public async Task<IActionResult> UpdateItem([FromBody] ItemDTO itemDTO, [FromRoute] int idItem)
        {
            try
            {
                var item = await _repository.UpdateItemAsync(idItem, itemDTO);

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
