using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class User 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}
        public string Name {get; set;}
        public string Surname {get; set;}
        public ICollection<Movie> Movies {get; set;}
    }
}