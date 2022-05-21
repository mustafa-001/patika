using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Fligth
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Plane Plane {get; set;} = null!;
        public Company Company {get; set;} = null!;
        public List<Pilot> Pilots {get; set;} = new List<Pilot>();
        public Airfield DepartureAirfield {get; set;} = null!;
        public Airfield ArrivalAirfield {get; set;} = null!;
        public DateTime DepartureTime {get; set;}
        public DateTime ArrivalTime {get; set;}
    }
}