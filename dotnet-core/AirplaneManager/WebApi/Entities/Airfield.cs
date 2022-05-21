using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Airfield
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = null!;
    }
}