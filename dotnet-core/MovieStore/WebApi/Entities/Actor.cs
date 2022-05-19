using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Actor 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}
        public string? Name {get; set;}
        public string? Surname {get; set;}
        public DateTime BirthDate {get; set;}
        public virtual ICollection<Movie> Movies {get; set;} = new List<Movie>();
    }
}