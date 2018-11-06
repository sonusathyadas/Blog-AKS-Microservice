using BlogService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace BlogService.Infrastructure
{
    public class BlogContext:DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> contextOptions)
            :base(contextOptions)
        {

        }

        public DbSet<Blog> Blogs { get; set; }
    }
}
