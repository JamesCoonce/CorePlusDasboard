using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models
{
    public class Practitioner
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PractitionerId { get; set; }
        public string Name { get; set; }

        public List<Appointment> Appointments { get; set; }
    }
}
