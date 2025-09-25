using System;
using System.Collections.Generic;

namespace _05_IntroToEF
{
    namespace AirportApp.Models
    {
        public class Flight
        {
            public int Id { get; set; }
            public string FlightNumber { get; set; } = null!;
            public DateTime DepartureTime { get; set; }
            public DateTime ArrivalTime { get; set; }
            public int DepartureCityId { get; set; }
            public City DepartureCity { get; set; } = null!;
            public int ArrivalCityId { get; set; }
            public City ArrivalCity { get; set; } = null!;
            public int AirplaneId { get; set; }
            public Airplane Airplane { get; set; } = null!;

            public ICollection<Account> Passengers { get; set; } = new List<Account>();
        }
    }
}
