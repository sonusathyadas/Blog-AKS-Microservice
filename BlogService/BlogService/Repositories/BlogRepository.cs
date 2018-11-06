using BlogService.Infrastructure;
using BlogService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogService.Repositories
{
    public class BlogRepository<T> : IBlogRepository<T> where T : Blog
    {
        private BlogContext db;
        private readonly DbSet<T> entities;

        public BlogRepository(BlogContext context)
        {
            this.db = context;
            this.entities = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return entities.ToList();
        }

        public IEnumerable<T> GetByAuthor(string author)
        {
            if (string.IsNullOrEmpty(author))
                return null;
            return entities.Where(b => b.Author == author)
                .ToList();
        }

        public T GetById(int id)
        {
            return entities.Find(id);
        }

        public async Task<T> AddAsync(T item)
        {
            var res=await entities.AddAsync(item);
            await this.db.SaveChangesAsync();
            return res.Entity as T ;
        }

        public async Task<T> DeleteAsync(int id)
        {
            var blog = entities.Find(id);
            if (blog == null)
                return null;
            entities.Remove(blog);
            await this.db.SaveChangesAsync();
            return blog;
        }

        public async Task<T> UpdateAsync(int id, T item)
        {
            if (id != item.Id)
                return null;
            var result = entities.Update(item);
            await this.db.SaveChangesAsync();
            return result.Entity;
        }
    }
}
