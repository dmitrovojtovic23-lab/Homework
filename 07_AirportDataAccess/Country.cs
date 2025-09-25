using System.Collections.Generic;

namespace _05_IntroToEF
{
    namespace AirportApp.Models
    {
        public class Country
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public ICollection<City> Cities { get; set; } = new List<City>();
        }
    }
}
