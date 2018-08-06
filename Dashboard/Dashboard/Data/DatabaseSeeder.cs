using Dashboard.Models;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Data
{
    public class DatabaseSeeder
    {
        public static void CreateAppointments(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var daysGenerator = new RandomGenerator();
                var priceGenerator = new RandomGenerator();

                if (!context.Practitioners.Any())
                { 
                    //{
                    //    var practitioners = new List<Practitioner>
                    //{
                    //    new Practitioner { Name = "Advent Medical"},
                    //    new Practitioner { Name = "Tampa General"},
                    //    new Practitioner { Name = "Florida Medcare"},
                    //    new Practitioner { Name = "Australian Med Corps"},
                    //    new Practitioner { Name = "Great Hospital"},
                    //    new Practitioner { Name = "UK MD"},
                    //    new Practitioner { Name = "Tokyo Hospital"},
                    //};

                    //    context.AddRange(practitioners);
                    //    context.SaveChanges();
                    var practitioners = context.Practitioners;
                    foreach (Practitioner practitioner in practitioners)
                    {
                        var appointments = Builder<Appointment>.CreateListOfSize(2000)
                                        .All()
                                            .With(a => a.ClientName = Faker.Company.Name())
                                            .With(a => a.AppointmentDate = DateTime.Now.AddDays(-daysGenerator.Next(1, 1100)))
                                            .With(a => a.Cost = priceGenerator.Next(50, 500))
                                            .With(a => a.Revenue = priceGenerator.Next(50, 500))
                                            .With(a => a.Time = daysGenerator.Next(5, 55))
                                            .With(a => a.MonthBilled = a.findMonthBilled(a.AppointmentDate))
                                            .With(a => a.Practitioner = practitioner)
                                       .Build();
                        context.AddRange(appointments);
                        context.SaveChanges();
                    }
                }
                
            }
        }
    }
}
