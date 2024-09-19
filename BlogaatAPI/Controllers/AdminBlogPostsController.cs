using BlogaatAPI.Data;
using BlogaatAPI.Models.Dtos;
using BlogaatAPI.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogaatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminBlogPostsController : ControllerBase
    {


        private readonly ApplicationDbcontet dbcontet;

        public AdminBlogPostsController(ApplicationDbcontet dbcontet11)
        {
            dbcontet = dbcontet11;
        }



        [HttpGet("All_Blog_Posts")]
        public IActionResult GetAll()
        {
            if (ModelState.IsValid)
            {
                var tag = dbcontet.BlogPosts./*Include(e => e).*/ToList();
                return Ok(tag);
            }
            return BadRequest(ModelState);

        }



        [HttpGet("{ID:Guid}", Name = "BlogPost_ID")]
        public IActionResult GeByID([FromRoute] Guid ID)
        {
            if (ModelState.IsValid)
            {
                var tag = dbcontet.BlogPosts./*Include(e => e.Tags).*/FirstOrDefault(e => e.Id == ID);/*Find(ID);*/
                return Ok(tag);
            }
            return BadRequest(ModelState);
        }




        [HttpPost]
        public IActionResult AddBlogPost([FromBody] AddBlogPostDto addBlogPostDto)
        {
            if (ModelState.IsValid)
            {
                var blogpost = new BlogPost
                {
                    Heading = addBlogPostDto.Heading,
                    PageTitle = addBlogPostDto.PageTitle,
                    Content = addBlogPostDto.Content,
                    ShortDescription = addBlogPostDto.ShortDescription,
                    FeateredImageUrl = addBlogPostDto.FeateredImageUrl,
                    UrlHandle = addBlogPostDto.UrlHandle,
                    PublishedDate = addBlogPostDto.PublishedDate,
                    Author = addBlogPostDto.Author,
                    Visible = addBlogPostDto.Visible
                    //tags= addBlogPostDto.tag_name,

                };
                //blogpost.Tags.Name=addBlogPostDto.tag_name;
                        dbcontet.BlogPosts.Add(blogpost);
                 dbcontet.SaveChanges();
                string url = Url.Link("BlogPost_ID", new { ID = blogpost.Id });
                return Created(url, blogpost);
            }
            return BadRequest(ModelState);
        }




        [HttpPut("{ID}")]
        public IActionResult UPDATA_BlogPost([FromRoute] Guid ID, [FromBody] AddBlogPostDto addBlogPostDto)
        {
            if (ModelState.IsValid)
            {
                var exsiting = dbcontet.BlogPosts.Find(ID); /*(editTagDto.Id);*/
                exsiting.Heading = addBlogPostDto.Heading;
                exsiting.PageTitle = addBlogPostDto.PageTitle;
                exsiting.Content = addBlogPostDto.Content;
                exsiting.ShortDescription = addBlogPostDto.ShortDescription;
                exsiting.FeateredImageUrl = addBlogPostDto.FeateredImageUrl;
                exsiting.UrlHandle = addBlogPostDto.UrlHandle;
                exsiting.PublishedDate = addBlogPostDto.PublishedDate;
                exsiting.Author = addBlogPostDto.Author;
                exsiting.Visible = addBlogPostDto.Visible;
                
                dbcontet.SaveChanges();
                return Ok(exsiting);
            }
            return BadRequest(ModelState);
        }




        [HttpDelete("{ID:Guid}")]
        public IActionResult Remove([FromRoute] Guid ID)
        {
            var exsiting = dbcontet.BlogPosts.Find(ID);
            if (exsiting != null)
            {
                dbcontet.BlogPosts.Remove(exsiting);
                dbcontet.SaveChanges();
                return Ok();
            }
            return NotFound();
        }


    }
}
