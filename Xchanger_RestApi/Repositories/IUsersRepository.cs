using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xchanger_RestApi.DTOs;
using Xchanger_RestApi.Models;

namespace Xchanger_RestApi.Repositories
{
    public interface IUsersRepository
    {

        public Task<IEnumerable<User>> GetUsersAsync();
        public Task<User> GetUserAsync(int idUser);
        public Task<User> RegisterUserAsync(UserRegisterDTO userDTO);
        public Task<User> GetUserByUsername(string login);
        public Task<User> GetUserByRefreshToken(string refreshToken);
        public Task SetUserRefreshTokenAsync(string userLogin, DateTime expireTime, DateTime createTime, string token);
        public Task<bool> CheckUserExistsAsync(string login);


    }
}
