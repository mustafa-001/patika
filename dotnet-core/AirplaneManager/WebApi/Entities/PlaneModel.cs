using System.ComponentModel.DataAnnotations.Schema;


namespace WebApi.Entities
{
    public class PlaneModel
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PilotNumber { get; set; }
        public int PassengerNumber { get; set; }

    }
}