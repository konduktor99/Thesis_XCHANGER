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
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Xchanger_RestApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    
    public class ItemsController : ControllerBase
    {

        private readonly IItemsRepository _repository;
        public static IWebHostEnvironment _env;

        public ItemsController(IItemsRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
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
                {
                    item.ImgBytesList = LoadFiles(item.Id);
                    return Ok(item);
                }
                else
                {
                    
                    return NotFound("Nie znaleziono przedmiotu");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Wystąpił błąd wewnętrzny serwera"+ex);

            }

        }

        [HttpGet("images/{idItem}")]
        public async Task<IActionResult> GetItemImageAsync([FromRoute] int idItem)
        {
            try
            {

                var imgBytesList = LoadFiles(idItem);
                return Ok(imgBytesList);
 

                return NotFound("Nie znaleziono zdjęć przedmiotu");

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Wystąpił błąd wewnętrzny serwera" + ex);

            }

        }

        [HttpPost("CreateItem")]
        //[DisableRequestSizeLimit,RequestFormLimits(MultipartBodyLengthLimit = int.MaxValue, ValueLengthLimit = int.MaxValue)]
        public async Task<IActionResult> CreateItem([FromForm] CreateItemDTO itemDTO)
        {
            try
            {
                var files = itemDTO.Files;
                itemDTO.Files = null;

                var item = await _repository.CreateItemAsync(itemDTO);

                saveFiles(files, item.Id);


                    return Ok(item);

            }
            catch (Exception ex)
            {
                return StatusCode(400, "Nieprawidłowe rządanie" + ex);
            }

        }

        [HttpPut("{idItem}")]
        public async Task<IActionResult> UpdateItem([FromForm] CreateItemDTO itemDTO, [FromRoute] int idItem)
        {
            try
            {
                var item = await _repository.UpdateItemAsync(idItem, itemDTO);


                if (item == null)
                    return NotFound("Nie znaleziono przedmiotu");

                var files = itemDTO.Files;
                itemDTO.Files = null;
                saveFiles(files, item.Id);

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

        private void saveFiles(IFormFile[] files, int itemId)
        {
            int counter = 1;
            if (files != null)
            {
                var path = Path.Combine(_env.WebRootPath, "itemPics", itemId.ToString());
                if (Directory.Exists(path))
                    Directory.Delete(path,true);
                    Directory.CreateDirectory(path);
                foreach (var file in files)
                {
                    string picName = "\\" + counter + "_itemPic_" + itemId + ".jpg";

                    if (System.IO.File.Exists(path + picName))
                        System.IO.File.Delete(path + picName);

                    using (FileStream fs = System.IO.File.Create(path + picName))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    counter++;
                }

            }

        }
        private List<byte[]> LoadFiles(int itemId)
        {

            var imgBytesList = new List<byte[]>();
            var itemFolderPath = Path.Combine(_env.WebRootPath, "itemPics", itemId.ToString());

            if (Directory.Exists(itemFolderPath))
                foreach (string file in Directory.EnumerateFiles(itemFolderPath, "*.jpg"))
                {
                    imgBytesList.Add(System.IO.File.ReadAllBytes(file));
                }
            return imgBytesList;
        }



    }


}
