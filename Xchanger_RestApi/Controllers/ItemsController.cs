using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xchanger_RestApi.DTOs;
using Xchanger_RestApi.Repositories;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Xchanger_RestApi.Helpers;
using Microsoft.AspNetCore.Authorization;

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
        
        public async Task<IActionResult> GetActiveItems([FromQuery] SearchDTO search)
        {

            try
            {

                var items = await _repository.GetActiveItemsAsync(null, null);
                

                if(search.Query!=null)
                {
                    if(search.Query.Length>2)
                    {
                        var keyWords = search.Query.ToLower().Split(null);
                        List<GetItemsDTO> filteredItems = new List<GetItemsDTO>();
                        foreach (var keyword in keyWords.Where(i => i != ""))
                        {
                            filteredItems.AddRange(items.Where(i => i.Title.ToLower().Contains(keyword)));
                        }
                        if (filteredItems.Count() > 0)
                            return Ok(filteredItems.Distinct().Select(i => { i.ImgBytes = LoadMainImage(i.Id); return i; }));
                    }
                    else
                    {
                        return NotFound("Nie znaleziono wyników dla \'"+ search.Query +"\'");
                    }
                    
                }
                else
                {
                    if (items.Count() > 0)
                        return Ok(items.Select(i => { i.ImgBytes = LoadMainImage(i.Id); return i; }));
                }
               
                    
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
                    return Ok(items.Select(i => { i.ImgBytes = LoadMainImage(i.Id); return i; }));
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
                    return Ok(items.Select(i => { i.ImgBytes = LoadMainImage(i.Id); return i; }));
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
                    return Ok(items.Select(i => { i.ImgBytes = LoadMainImage(i.Id); return i; }));
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
                    item.ImgBytesList = LoadImages(item.Id);
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

                var imgBytesList = LoadImages(idItem);
                return Ok(imgBytesList);
 

                return NotFound("Nie znaleziono zdjęć przedmiotu");

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Wystąpił błąd wewnętrzny serwera" + ex);

            }

        }

        [HttpPost("CreateItem")]
        [Authorize]
        public async Task<IActionResult> CreateItem([FromForm] CreateItemDTO itemDTO)
        {
            try
            {
                var userId = JwtDecoder.GetClaimFromJwt(Request.Headers["authorization"].ToString().Substring(7),"id");

                if (userId == null)
                    return Unauthorized();

                var files = itemDTO.Files;
                itemDTO.Files = null;

                var item = await _repository.CreateItemAsync(itemDTO, (int)userId);

                saveImages(files, item.Id);

                return Ok(item);

            }
            catch (Exception ex)
            {
                return StatusCode(400, "Nieprawidłowe rządanie" + ex);
            }

        }
        [HttpPut("{idItem}")]
        [Authorize]
        public async Task<IActionResult> UpdateItem([FromForm] CreateItemDTO itemDTO, [FromRoute] int idItem)
        {
            
            try
            {
                var userId = JwtDecoder.GetClaimFromJwt(Request.Headers["authorization"].ToString().Substring(7), "id");
                if (userId == null)
                    return Unauthorized();

                var item = await _repository.GetItemAsync(idItem);
                if (userId != item.UserId)
                    return Unauthorized("Nie można ingerować w ogłoszenie innego użytkownika");

                if (item == null)
                    return NotFound("Nie znaleziono przedmiotu");

                var updatedItem = await _repository.UpdateItemAsync(item, itemDTO);



                var files = itemDTO.Files;
                itemDTO.Files = null;
                saveImages(files, updatedItem.Id);

                return Ok(updatedItem);

            }
            catch (Exception ex)
            {
                return StatusCode(400, "Nieprawidłowe rządanie");
            }

        }
        [HttpDelete("DeleteItem/{idItem}")]
        [Authorize]
        public async Task<IActionResult> DeleteItem([FromRoute] int idItem)
        {
            try
            {
                var userId = JwtDecoder.GetClaimFromJwt(Request.Headers["authorization"].ToString().Substring(7), "id");
                if (userId == null)
                    return Unauthorized();

                var item = await _repository.GetItemAsync(idItem);
                    if (userId != item.UserId)
                        return Unauthorized("Nie można ingerować w ogłoszenie innego użytkownika");


                var deletedItem = await _repository.DeleteItem(idItem);

                var path = Path.Combine(_env.WebRootPath, "itemPics", idItem.ToString());
                if (Directory.Exists(path))
                    Directory.Delete(path, true);

                if (item == null)
                    return NotFound("Nie znaleziono przedmiotu");

                return Ok(deletedItem);

        }
            catch (Exception ex)
            {
                return StatusCode(400, "Nieprawidłowe rządanie");
            }


        }

        private void saveImages(IFormFile[] files, int itemId)
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
        private List<byte[]> LoadImages(int itemId)
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

        private byte[] LoadMainImage(int itemId)
        {

            byte[] imgBytes = null; 
            var itemFolderPath = Path.Combine(_env.WebRootPath, "itemPics", itemId.ToString());

            string file;
            if (Directory.Exists(itemFolderPath))
            {
                file = Directory.EnumerateFiles(itemFolderPath, "*.jpg").FirstOrDefault();
                if(file!=null)
                    imgBytes = System.IO.File.ReadAllBytes(file);
            }
                 
                
            return imgBytes;
        }



    }


}
