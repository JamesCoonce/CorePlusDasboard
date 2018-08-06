using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Models;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts(int startDateIndex)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index + startDateIndex).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        [HttpGet("[action]")]
        public IEnumerable<Appointment> Appointments()
        {
            var daysGenerator = new RandomGenerator();
            var priceGenerator = new RandomGenerator();
            var practitioner = new Practitioner();
            practitioner.PractitionerId = 3;
            practitioner.Name = "Bob the Man";

            var appointments = Builder<Appointment>.CreateListOfSize(200)
                                .All()
                                    .With(a => a.ClientName = Faker.Company.Name())
                                    .With(a => a.AppointmentDate = DateTime.Now.AddDays(-daysGenerator.Next(1, 100)))
                                    .With(a => a.Cost = priceGenerator.Next(50, 500))
                                    .With(a => a.Revenue = priceGenerator.Next(50, 500))
                                    .With(a => a.MonthBilled = a.findMonthBilled(a.AppointmentDate))
                                    .With(a => a.PractitionerId = practitioner.PractitionerId)
                                    .With(a => a.Practitioner = practitioner)
                               .Build();
            return appointments;
        }

        [HttpGet("[action]")]
        public IEnumerable<Practitioner> Practitioners()
        {
            List<Practitioner> practitioners = new List<Practitioner>
            {
                new Practitioner { PractitionerId = 101, Name = "Advent Medical"},
                new Practitioner { PractitionerId = 102, Name = "Tampa General"},
                new Practitioner { PractitionerId = 103, Name = "Florida Medcare"},
                new Practitioner { PractitionerId = 104, Name = "Australian Med Corps"},
                new Practitioner { PractitionerId = 105, Name = "Great Hospital"},
                new Practitioner { PractitionerId = 106, Name = "UK MD"},
                new Practitioner { PractitionerId = 107, Name = "Tokyo Hospital"},
            };

            return practitioners;
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}
