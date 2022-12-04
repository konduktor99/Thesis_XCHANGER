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
    [Route("[controller]")]
    [ApiController]
    
    public class ItemsController : ControllerBase
    {

        private readonly IItemsRepository _repository;

        public ItemsController(IItemsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveItems()
        {

            try
            {
                var items = await _repository.GetActiveItemsAsync(null, null);

                if (items.Count() > 0)
                    return Ok(items);
                else
                    return NotFound("Nie znaleziono przedmiotów");

            }
            catch (Exception)
            {
                return StatusCode(500, "Wystąpił błąd wewnętrzny serwera");

            }


        }
        [HttpGet("category/{category?}")]
        public async Task<IActionResult> GetActiveItemsByCat([FromRoute] string? category =null)
        {

            try
            {
                var items = await _repository.GetActiveItemsAsync(category, null);

                if (items.Count() > 0)
                    return Ok(items);
                else
                    return NotFound("Nie znaleziono przedmiotów");

            }
            catch (Exception)
            {
                return StatusCode(500, "Wystąpił błąd wewnętrzny serwera");

            }
        }
        [HttpGet("user/{user?}")]
      
        public async Task<IActionResult> GetActiveItemsByUsr([FromRoute] string? user = null)
        {

            try
            {
                var items = await _repository.GetActiveItemsAsync(null, user);

                if (items.Count() > 0)
                    return Ok(items);
                else
                    return NotFound("Nie znaleziono przedmiotów");

            }
            catch (Exception)
            {
                return StatusCode(500, "Wystąpił błąd wewnętrzny serwera");
            }

        }
        [HttpGet("category/{category?}/user/{user?}")]

        public async Task<IActionResult> GetActiveItemsByCatUsr([FromRoute] string? category = null, [FromRoute] string? user = null)
        {

            try
            {
                var items = await _repository.GetActiveItemsAsync(category, user);

                if (items.Count() > 0)
                    return Ok(items);
                else
                    return NotFound("Nie znaleziono przedmiotów");

            }
            catch (Exception)
            {
                return StatusCode(500, "Wystąpił błąd wewnętrzny serwera");
            }

        }


        [HttpGet("{idItem}")]
        public async Task<IActionResult> GetItemDtoAsync([FromRoute] int idItem)
        {
            try
            {
                var item =await _repository.GetItemDtoAsync(idItem);

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
                return StatusCode(400, "Nieprawidłowe rządanie" + ex);
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
                return StatusCode(400, "Nieprawidłowe rządanie" + ex);
            }

        }
        [HttpDelete("DeleteItem/{idItem}")]
        public async Task<IActionResult> DeleteItem([FromRoute] int idItem)
        {
            try
            {
                var item = await _repository.DeleteItem(idItem);

                if (item == null)
                    return NotFound("Nie znaleziono przedmiotu");

                return Ok(item);

        }
            catch (Exception ex)
            {
                return StatusCode(400, "Nieprawidłowe rządanie" + ex);
    }


}



    }


}
