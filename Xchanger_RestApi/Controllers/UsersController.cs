using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xchanger_RestApi.Models;
using Xchanger_RestApi.Repositories;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;

namespace Xchanger_RestApi.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUsersRepository _repository;
        private readonly IConfiguration _configuration;

        public UsersController(IUsersRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterDTO userDTO)
        {
            if (await _repository.CheckUserExistsAsync(userDTO.Login))
                return Conflict("Użytkownik o takim loginie już istnieje");

                var user = new User();
            try
            {
                user = await _repository.RegisterUserAsync(userDTO);


                var jwt = GenerateToken(user);

                var refreshToken = GenerateRefreshToken();
                await SetRefreshToken(refreshToken, user.Login);

                return Ok(jwt);

            }
            catch (Exception ex)
            {
                return StatusCode(400, "Nieprawidłowe rządanie \n" + ex);
            }

        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(UserLoginDTO userDto)
        {

            var user = await _repository.GetUserByUsername(userDto.Login);


            if (user == null || !VerifyPassword(userDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Nieprawidłowa nazwa użytkownika lub hasło.");
            }

            var jwt = GenerateToken(user);

            var refreshToken = GenerateRefreshToken();
            await SetRefreshToken(refreshToken, user.Login);

            return Ok(jwt);
        }

        [HttpPost("Logout")]
        public async Task<ActionResult<string>> Logout()
        {

            Response.Cookies.Append("refreshToken", "", new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddMinutes(-1),
                SameSite = SameSiteMode.None,
                Secure = true

            });

            Response.Cookies.Append("signed", "", new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(-1),
                SameSite = SameSiteMode.None,
                Secure = true
            });

            return Ok("Wylogowano");
        }

        private bool VerifyPassword(string password, byte[] userPasswordHash, byte[] userPasswordSalt)
        {
            using (var hmacAlg = new HMACSHA512(userPasswordSalt))
            {
                byte[] computedHash = hmacAlg.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(userPasswordHash);
            }
        }

        private string GenerateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("name", user.Login),
                new Claim("id", user.Id.ToString()),
                new Claim("role", "User")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("Security:Key").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials);

            var jwtString = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtString;
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {


            var refreshToken = Request.Cookies["refreshToken"];
            if (refreshToken == null)
                return Unauthorized("Zaloguj się");

            var user = await _repository.GetUserByRefreshToken(refreshToken);
            if (user == null)
                return Unauthorized("Zaloguj się");

            else if (user.RefreshTokenExpireTime < DateTime.Now)
            {
                return Unauthorized("Zaloguj się");
            }

            string token = GenerateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            await SetRefreshToken(newRefreshToken, user.Login);

            return Ok(token);
        }

        [HttpPost("Current")]
        public async Task<ActionResult<string>> GetUserByRefreshToken()
        {


            var refreshToken = Request.Cookies["refreshToken"];
            if (refreshToken == null)
                return NotFound();

            var user = await _repository.GetUserByRefreshToken(refreshToken);
            if (user == null)
                return NotFound();

            return Ok(user.Login);
        }

        private RefreshToken GenerateRefreshToken()
        {

            string refreshTokenString;
            byte[] refreshTokenBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(refreshTokenBytes);
                refreshTokenString = Convert.ToBase64String(refreshTokenBytes);
            }
            var refreshToken = new RefreshToken
            {
                Token = refreshTokenString,
                ExpireTime = DateTime.Now.AddMinutes(15),
                CreateTime = DateTime.Now
            };

            return refreshToken;
        }

        private async Task SetRefreshToken(RefreshToken newRefreshToken, string userLogin)
        {

            Response.Cookies.Append("refreshToken", newRefreshToken.Token, new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.ExpireTime,
                SameSite = SameSiteMode.None,
                Secure = true

            });

            Response.Cookies.Append("signed", "true", new CookieOptions
            {
                Expires = newRefreshToken.ExpireTime,
                SameSite = SameSiteMode.None,
                Secure = true
            });

            await _repository.SetUserRefreshTokenAsync(userLogin, newRefreshToken.ExpireTime, newRefreshToken.CreateTime, newRefreshToken.Token);
            //user.RefreshTokenExpireTime = newRefreshToken.ExpireTime;
            //user.RefreshTokenCreateTime = newRefreshToken.CreateTime;
            //user.RefreshToken = newRefreshToken.Token;


        }

        //[HttpPut("{idItem}")]
        //public async Task<IActionResult> UpdateItem([FromBody] ItemDTO itemDTO, [FromRoute] int idItem)
        //{
        //    try
        //    {
        //        var item = await _repository.UpdateItemAsync(idItem, itemDTO);

        //        if (item == null)
        //            return NotFound("Nie znaleziono przedmiotu");

        //        return Ok(item);

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "Wystąpił błąd wewnętrzny serwera \n" + ex);
        //    }

        //}

        [HttpGet("{login}")]
        public async Task<IActionResult> GetItemDtoAsync([FromRoute] string login)
        {
            try
            {
                var user = await _repository.GetUserByUsername(login);

                if (user != null)
                    return Ok(user);
                else
                    return NotFound("Nie znaleziono użytkownika");

            }
            catch (Exception)
            {
                return StatusCode(500, "Wystąpił błąd wewnętrzny serwera");

            }

        }

    }
}
