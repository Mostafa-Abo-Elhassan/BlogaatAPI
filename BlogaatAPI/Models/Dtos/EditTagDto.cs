namespace BlogaatAPI.Models.Dtos
{
    public class EditTagDto
    {
        public Guid Id { get; set; }

        //[Required]
        //[StringLength(10)]
        public string Name { get; set; }
        //[StringLength(60)]
        //[Required]
        public string DisplayName { get; set; }
    }
}
