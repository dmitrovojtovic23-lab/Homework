using System.Collections.Generic;

namespace _05_IntroToEF
{
    namespace AirportApp.Models
    {
        public class AirplaneType
        {
            public int Id { get; set; }
            public string ModelName { get; set; } = null!;
            public int Capacity { get; set; }
            public ICollection<Airplane> Airplanes { get; set; } = new List<Airplane>();
        }
    }
}
