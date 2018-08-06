using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models
{
    interface IAppointmentRepository
    {
        IEnumerable<Appointment> GetAppointments(int practitionerId, DateTime startTime, DateTime endTime);
        Task<Appointment> GetAppointment(int AppointmentId);
        Task SaveAppointmentAsync(Appointment appointment);
        Task UpdateAppointmentAsync(Appointment appointment);
        Task DeleteAppointmentAsync(int AppointmentId);
    }
}
