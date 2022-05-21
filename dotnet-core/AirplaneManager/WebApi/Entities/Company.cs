using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Company
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name {get; set;} = String.Empty;
        public List<Fligth> Fligths {get; set;} = new List<Fligth>();
    }
}