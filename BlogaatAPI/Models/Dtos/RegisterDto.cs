using System.ComponentModel.DataAnnotations;

namespace BlogaatAPI.Models.Dtos
{
    public class RegisterDto
    {

        public string UserName { get; set; }
   
        public string Email { get; set; }

    
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
