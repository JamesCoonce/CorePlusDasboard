using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models.ViewModels
{
    public class AppointmentsViewModel
    {
        public IEnumerable<Appointment> Appointments { get; set; }
        public double TotalCost { get; set; }
        public double TotalRevenue { get; set; }
        public double GrossProfit { get; set; }
        public double ProfitMargin { get; set; }

        public AppointmentsViewModel(IEnumerable<Appointment> appointments)
        {
            this.Appointments = appointments;
            this.TotalCost = getTotalCost(this.Appointments);
            this.TotalRevenue = getTotalRevenue(this.Appointments);
            this.GrossProfit = getProfit(this.TotalRevenue, this.TotalCost);
            this.ProfitMargin = getProfitMargin(this.GrossProfit, this.TotalRevenue);
        }

        private double getTotalCost(IEnumerable<Appointment> appointments)
        {
            double totalCost = 0;
            foreach(Appointment appointment in appointments)
            {
                var cost = appointment.Cost;
                totalCost = totalCost + cost;
            }
            return totalCost; 
        }

        private double getTotalRevenue(IEnumerable<Appointment> appointments)
        {
            double totalRevenue = 0;
            foreach (Appointment appointment in appointments)
            {
                var revenue = appointment.Revenue;
                totalRevenue = totalRevenue + revenue;
            }
            return totalRevenue;
        }

        private double getProfit(double totalRevenue, double totalCost)
        {
            double profit = totalRevenue - totalCost;

            return profit;
        }

        private double getProfitMargin(double grossProfit, double revenue)
        {
            double profitMargin = (grossProfit / revenue) * 100;

            return profitMargin;
        }
    }
}
