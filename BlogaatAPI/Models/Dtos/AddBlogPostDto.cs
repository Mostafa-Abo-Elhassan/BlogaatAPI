using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogaatAPI.Models.Dtos
{
    public class AddBlogPostDto
    {
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeateredImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }
        //public string tag_name { get; set; }



      

    }
}
