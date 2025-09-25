using System.Collections.Generic;

namespace _05_IntroToEF
{
    namespace AirportApp.Models
    {
        public class Airplane
        {
            public int Id { get; set; }
            public string Registration { get; set; } = null!;

            public int AirplaneTypeId { get; set; }
            public AirplaneType Type { get; set; } = null!;

            public ICollection<Flight> Flights { get; set; } = new List<Flight>();
        }
    }
}
