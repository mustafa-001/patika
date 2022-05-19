using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}
        public string Name {get; set;} = null!;
        public DateTime  Date {get; set;}
        public string? Genre {get; set;}
        public int? DirectorId {get; set;}
        public Director? Director {get; set;}
        public ICollection<Actor> Actors {get; set;} = new List<Actor>();
        public Decimal Price {get; set;}

    }
}