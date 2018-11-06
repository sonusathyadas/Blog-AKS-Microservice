using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogService.Models;
using BlogService.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class BlogsController : ControllerBase
    {
        private IBlogRepository<Blog> repository;

        public BlogsController(IBlogRepository<Blog> repository)
        {
            this.repository = repository;
        }

        //GET /api/v1/blogs
        [ProducesResponseType(200)]
        [HttpGet("", Name ="GetBlogs")]
        public IEnumerable<Blog> GetBlogs()
        {
            return this.repository.GetAll();
        }

        //GET /api/v1/blogs/author/{author}
        [ProducesResponseType(200)]
        [HttpGet("author/{author}", Name ="GetBlogsByAuthor")]
        public IEnumerable<Blog> GetBlogsByAuthor(string author)
        {
            return this.repository.GetByAuthor(author);
        }

        //GET /api/v1/blogs/{id}
        [ProducesResponseType(200)]
        [HttpGet("{id}", Name ="GetBlogById")]
        public Blog GetBlogsById(int id)
        {
            return this.repository.GetById(id);
        }

        //POST /api/v1/blogs
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost("", Name ="AddBlog")]
        public async Task<ActionResult<Blog>> AddBlog([FromBody]Blog model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var blog=await this.repository.AddAsync(model);
            return blog;
        }

        //PUT /api/v1/blogs/{id}
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [HttpPut("{id}", Name = "UpdateBlog")]
        public async Task<ActionResult<Blog>> UpdateBlog(int id, [FromBody]Blog model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var blog = await this.repository.UpdateAsync(id,model);
            if (blog == null)
                return NotFound("Blog not found with the given id");
            else
                return blog;
        }

        //DELETE /api/v1/blogs/{id}
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [HttpDelete("{id}", Name = "DeleteBlog")]
        public async Task<ActionResult<Blog>> DeleteBlog(int? id)
        {
            if (!id.HasValue)
                return BadRequest();
            var blog = await this.repository.DeleteAsync(id.Value);
            if (blog == null)
                return NotFound("Blog not found with the given id");
            else
                return blog;
        }
    }
}