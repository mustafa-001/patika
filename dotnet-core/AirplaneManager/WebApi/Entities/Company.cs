using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Company
    {
        [Key]
        public string Name {get; set;} = String.Empty;
        public List<Fligth> Fligths {get; set;} = new List<Fligth>();
    }
}