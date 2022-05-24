using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Plane
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public PlaneModel Model { get; set; } = null!;
        public bool isWorking { get; set; } = true;
    }
}
