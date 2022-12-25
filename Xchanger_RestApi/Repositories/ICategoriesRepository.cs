using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xchanger_RestApi.DTOs;
using Xchanger_RestApi.Models;

namespace Xchanger_RestApi.Repositories
{
    public interface ICategoriesRepository
    {

        public Task<IEnumerable<GetCategoryDTO>> GetCategoriesAsync();
       
    }
}
