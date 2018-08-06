using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Data;
using Dashboard.Models;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SampleDataController(ApplicationDbContext context)
        {
            _context = context;
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
            //List<Practitioner> practitioners = new List<Practitioner>
            //{
            //    new Practitioner { PractitionerId = 101, Name = "Advent Medical"},
            //    new Practitioner { PractitionerId = 102, Name = "Tampa General"},
            //    new Practitioner { PractitionerId = 103, Name = "Florida Medcare"},
            //    new Practitioner { PractitionerId = 104, Name = "Australian Med Corps"},
            //    new Practitioner { PractitionerId = 105, Name = "Great Hospital"},
            //    new Practitioner { PractitionerId = 106, Name = "UK MD"},
            //    new Practitioner { PractitionerId = 107, Name = "Tokyo Hospital"},
            //};

            return _context.Practitioners;
        }

        [HttpGet("practitioners/{id}")]
        public async Task<IActionResult> GetPractitioner([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var practitioner = await _context.Practitioners.SingleOrDefaultAsync(m => m.PractitionerId == id);

            if (practitioner == null)
            {
                return NotFound();
            }

            return Ok(practitioner);
        }

    }
}
