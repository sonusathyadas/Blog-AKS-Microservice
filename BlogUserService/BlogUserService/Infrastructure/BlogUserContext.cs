using BlogUserService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogUserService.Infrastructure
{
    public class BlogUserContext:DbContext
    {
        public BlogUserContext(DbContextOptions<BlogUserContext> options)
            :base(options)
        {

        }

        public DbSet<BlogUser> Users { get; set; }
    }
}
