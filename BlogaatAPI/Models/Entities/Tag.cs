using System.Text.Json.Serialization;

namespace BlogaatAPI.Models.Entities
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }

        public ICollection<BlogPost>? BlogPosts { get; set; }/* public virtual List<BlogPost>? blogPosts { get; set; */
    }
    

    //[JsonIgnore]

}

