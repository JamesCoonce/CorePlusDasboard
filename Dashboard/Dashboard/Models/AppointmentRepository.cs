using Dashboard.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task DeleteAppointmentAsync(int AppointmentId)
        {
            var appointment = await GetAppointment(AppointmentId);
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task<Appointment> GetAppointment(int AppointmentId)
        {
            return await _context.Appointments.SingleOrDefaultAsync(a => a.AppointmentId == AppointmentId);
        }

        public IEnumerable<Appointment> GetAppointments(int practitionerId, DateTime startTime, DateTime endTime)
        {
            var practitioner = new Practitioner();
            practitioner.PractitionerId = 3;
            practitioner.Name = "Bob the Man";
            var appointments = _context.Appointments
                                    .Where(a => a.PractitionerId == practitionerId)
                                    .Where(a => a.AppointmentDate > startTime)
                                    .Where(a => a.AppointmentDate < endTime)
                                    .ToList();
            return appointments;
        }

        public async Task SaveAppointmentAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            await _context.SaveChangesAsync();
        }

        
    }
}
