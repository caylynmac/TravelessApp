//using Android.Hardware.Lights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TravelessApp.Models
{
    public class Flight
    {
        //make private??
        public string FlightCode { get; set; }
        public string Airline { get; set; }
        public string From { get; set; } // origin city
        public string To { get; set; } //destination city
        public string Day { get; set; }
        public string Time { get; set; } //departure time HH:MM
        public string Cost { get; set; }
        public bool IsFullyBooked { get; set; } // Property to indicate if the flight is fully booked


        
        public override string ToString() {
            return String.Format("{0,-10}{1,-15}${2,-10}{3, -20}{4,-20}{5,-20}", Time, Day, Cost, Airline, "Origin: "+ From, "Destination: " + To); // {index, alignment}

        }
    }
}
