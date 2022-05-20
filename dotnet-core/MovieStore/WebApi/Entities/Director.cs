using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Director 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}
        public string Name {get; set;}=String.Empty;
        public string? Surname {get; set;}
        public DateTime BirthDate {get; set;}
        public ICollection<Movie> Movies {get; set;} = new List<Movie>();
    }
}