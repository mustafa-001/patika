using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class User 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}
        public string Name {get; set;} = String.Empty;
        public string Surname {get; set;} = String.Empty;
        public string Email {get; set;} = String.Empty;
        public string Password {get; set;} = String.Empty;
        public ICollection<Movie> Movies {get; set;} = new List<Movie>();
        public string? RefreshToken {get; set;}
        public DateTime  RefreshTokenExpireDate {get; set;}
    }
}