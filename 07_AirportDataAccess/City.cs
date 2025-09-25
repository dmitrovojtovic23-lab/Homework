using System.Collections.Generic;

namespace _05_IntroToEF
{
    namespace AirportApp.Models
    {
        public class City
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;

            public int CountryId { get; set; }
            public Country Country { get; set; } = null!;
            public ICollection<Flight> Departures { get; set; } = new List<Flight>();
            public ICollection<Flight> Arrivals { get; set; } = new List<Flight>();
        }
    }
}
