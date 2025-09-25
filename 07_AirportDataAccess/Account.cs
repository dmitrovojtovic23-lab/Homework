using _05_IntroToEF.AirportApp.Models.AirportApp.Models;
using System.Collections.Generic;

namespace _05_IntroToEF
{
    namespace AirportApp.Models
    {
        public class Account
        {
            public int Id { get; set; }
            public string Login { get; set; } = null!;
            public string PasswordHash { get; set; } = null!;

            // One-to-zero-or-one
            public AccountDetail? Detail { get; set; }

            // Many-to-many: accounts booked flights
            public ICollection<Flight> Flights { get; set; } = new List<Flight>();
        }

    }
}
