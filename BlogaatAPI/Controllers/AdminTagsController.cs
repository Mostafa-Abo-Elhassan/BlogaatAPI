using BlogaatAPI.Data;
using BlogaatAPI.Models.Dtos;
using BlogaatAPI.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogaatAPI.Controllers
{
    //[Authorize("Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminTagsController : ControllerBase
    {

        private readonly ApplicationDbcontet dbcontet;

        public AdminTagsController(ApplicationDbcontet dbcontet11)
        {
            dbcontet = dbcontet11;
        }


        [Authorize]
        [HttpGet("All_Tags")]
        public IActionResult getAll()
        {
            if (ModelState.IsValid)
            {
                var  tag = dbcontet.Tags/* .Include(e=>e.blogPosts)*/.ToList();
                return Ok(tag);
            }
            return BadRequest(ModelState);

        }



        [HttpGet("{ID:Guid}", Name = "Tag_ID")]
        public IActionResult GeByID([FromRoute] Guid ID)
        {
            if (ModelState.IsValid)
            {
                var tag = dbcontet.Tags/*.Include(e => e.blogPosts).*/.FirstOrDefault(e => e.Id == ID); /*.Find(ID);*/
                return Ok(tag);
            }
            return BadRequest(ModelState);
        }




        [HttpPost]
        public IActionResult AddTag([FromBody]  AddTagDto addTagDto)
        {
            if (ModelState.IsValid)
            {
                var tag = new Tag()
                {
                Name = addTagDto.Name,
                DisplayName= addTagDto.DisplayName,
               

                };
                dbcontet.Tags.Add(tag);

                dbcontet.SaveChanges();
                string url = Url.Link("Tag_ID", new { ID = tag.Id });
                return Created(url, tag);
            }
            return BadRequest(ModelState);
        }




        [HttpPut("{ID}")]
        public IActionResult UPDATAtag([FromRoute] Guid ID, [FromBody] EditTagDto editTagDto)
        {
            if (ModelState.IsValid)
            {
                var tag = dbcontet.Tags.Find(ID); /*(editTagDto.Id);*/
                tag.Name = editTagDto.Name;
                tag.DisplayName = editTagDto.DisplayName;
                dbcontet.SaveChanges();
                return Ok(tag);
            }
            return BadRequest(ModelState);
        }




        [HttpDelete("{ID:Guid}")]
        public IActionResult remove([FromRoute] Guid ID)
        {
            var tag = dbcontet.Tags.Find(ID);
            if (tag != null)
            {
                dbcontet.Tags.Remove(tag);
                dbcontet.SaveChanges();
                return Ok();
            }
            return NotFound();
        }






    }
}
