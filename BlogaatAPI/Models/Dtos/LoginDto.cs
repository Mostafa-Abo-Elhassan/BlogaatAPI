using System.ComponentModel.DataAnnotations;

namespace BlogaatAPI.Models.Dtos
{
    public class LoginDto
    {

      
       
        public string UserName { get; set; }

        
        [DataType(DataType.Password)]
        public string Password { get; set; }

   
        //public bool RememberMe { get; set; }

        //public string? ReturnUrl { get; set; }
    }
}
