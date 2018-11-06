using BlogService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogService.Repositories
{
    public interface IBlogRepository<T>:IRepository<T> 
    {
        IEnumerable<T> GetByAuthor(string author);
    }
}
