using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Movie
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}
        public string? Name {get; set;}
        public DateOnly  Year {get; set;}
        public string? Genre {get; set;}
        public Director? Director {get; set;}
        public ICollection<Actor>? Actors {get; set;}
        public Decimal Price {get; set;}

    }
}