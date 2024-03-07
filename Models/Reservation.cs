using System;

namespace TravelessApp.Models
{
    public class Reservation
    {
        public string ReservationCode { get; set; }
        public Flight ReservedFlight { get; set; }
        public string TravelerName { get; set; }
        public string Citizenship { get; set; }
       

        // Constructor
        public Reservation(string reservationCode, Flight reservedFlight, string travelerName, string citizenship)
        {
            ReservationCode = reservationCode;
            ReservedFlight = reservedFlight;
            TravelerName = travelerName;
            Citizenship = citizenship;
        }
    }
}
