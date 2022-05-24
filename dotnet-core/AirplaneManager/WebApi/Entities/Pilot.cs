using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Pilot
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public List<Fligth> Fligths { get; set; } = new List<Fligth>();
    }
}