using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models
{
    public class Appointment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppointmentId { get; set; }
        public string ClientName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime MonthBilled { get; set; }
        public int Time { get; set; }
        public int Cost { get; set; }
        public int Revenue { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PractitionerId { get; set; }
        public Practitioner Practitioner { get; set; }

        public DateTime findMonthBilled(DateTime month)
        {
            DateTime date = month;
            var monthBilled = new DateTime(date.Year, date.Month, 1);
            return monthBilled;
        }
    }

}