using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xchanger_RestApi.DTOs;
using Xchanger_RestApi.Models;

namespace Xchanger_RestApi.Repositories
{
    public class UsersRepository : IUsersRepository
    {

        private XchangerDbContext _dbContext;

        public UsersRepository(XchangerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> RegisterUserAsync(UserRegisterDTO userDTO)
        {
            User user = new User();
            byte[] passHash, passSalt;

            using (var hmacsha512 = new HMACSHA512())
            {
                passSalt = hmacsha512.Key;
                passHash = hmacsha512.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
            }

            user.PasswordHash = passHash;
            user.PasswordSalt = passSalt;
            user.Login = userDTO.Login;
            user.Email = userDTO.Email;
            user.PhoneNumber = userDTO.PhoneNumber;
            user.JoinDate = DateTime.Now;

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }


        //public async Task<User> LoginUserAsync(User user, UserLoginDTO userDTO)
        //{
        //    using (var hmac = new HMACSHA512(user.PasswordSalt))
        //    {
        //        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userDTO.Password));
        //        computedHash.SequenceEqual(user.PasswordHash);
        //    }


        //    await _dbContext.Users.AddAsync(user);
        //    await _dbContext.SaveChangesAsync();
        //    return user;
        //}

        public async Task<User> GetUserByUsername(string login)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Login == login);
        }

        public async Task<User> GetUserByRefreshToken(string refreshToken)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }

        public async Task<User> GetUserAsync(int idUser)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(i => i.Id == idUser);
            return user;
        }

        public async Task<bool> CheckUserExistsAsync(string login)
        {
            return await _dbContext.Users.AnyAsync(i => i.Login == login);
            
        }

        public async Task SetUserRefreshTokenAsync(string userLogin, DateTime expireTime, DateTime createTime, string token)
        {
            var user = await GetUserByUsername(userLogin);
            user.RefreshTokenExpireTime = expireTime;
            user.RefreshTokenCreateTime = createTime;
            user.RefreshToken = token;

            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }



}
}
