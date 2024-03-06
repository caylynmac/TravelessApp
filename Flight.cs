﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelessApp.Models
{
    class Flight
    {
        //make private??
        public string FlightCode { get; set; }
        public string Airline { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public string Cost { get; set; }
        public bool IsFullyBooked { get; set; } // Property to indicate if the flight is fully booked



    }
}
