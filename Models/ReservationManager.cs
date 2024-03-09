//using Intents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
//using static ObjCRuntime.Dlfcn;
using System.Xml.Linq;

//the reservations list is stored here

// When a travel agent selects a flight from the list, the text fields are populated with the selected
// flight code, airline, day, time, and cost. The travel agent enters the traveler’s full name and
// citizenship. The flight code, airline, day, time, and cost cannot be edited. An error message
// is displayed if:
// • A reservation is to be made but no flight is selected
// • The name field is empty
// • The citizenship field is empty

namespace TravelessApp.Models
{
    class ReservationManager
    { 

        // list to store reservations with specified parameters
        public static List<Reservation> madeReservations = new List<Reservation>();

        // The makeReservation method receives as its input arguments: a Flight object, the traveler's
        // name, and citizenship. An exception is thrown if the flight is completely booked, or the
        // flight is null, or the name is empty/null, or the citizenship is empty/null. If there are no
        // exceptions thrown, a Reservation object is created, saved to the binary file, and returned by
        // the method.
        public static Reservation MakeReservation(Flight flight, string travelerName, string citizenship)
        {
            // Check for null flight
            if (flight == null)
                throw new ArgumentNullException(nameof(flight), "Flight cannot be null");

            // Check if flight is fully booked
            if (flight.IsFullyBooked)
                throw new InvalidOperationException("The flight is fully booked");

            // Check for empty traveler name
            if (string.IsNullOrWhiteSpace(travelerName))
                throw new ArgumentException("Traveler's name cannot be empty", nameof(travelerName));

            // Check for empty citizenship
            if (string.IsNullOrWhiteSpace(citizenship))
                throw new ArgumentException("Citizenship cannot be empty", nameof(citizenship));

            // Create and return a new reservation, add it to list
            Reservation reservation = new Reservation(flight, travelerName, citizenship);
            madeReservations.Add(reservation);

            //write to file
            //const string filepath for reservations file
            string RESFILEPATH = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "../../../../../../Models/reservations.bin";

            // Writing binary data
            using (BinaryWriter writer = new BinaryWriter(File.Open(RESFILEPATH, FileMode.Append)))
            {
                string res = reservation.ToString();
                writer.Write(res);
            }

            return reservation; //so the data can be accessed by the page if necessary
        }

        //find matching reservations from search page
        public static List<Reservation> FindReservations(string code, string airline, string customerName)
        {
            //if any of the input fields were not filled in, reset them to be length 2 or length 5 for day
            //so that all flight objects with any origin and/or destination and/or day can be returned
            if (string.IsNullOrEmpty(code)) { code = "xxxxxx"; } //length 6 so it becomes length 7 in the ordinal clause
            if (airline ==  null) { airline = ""; } //an empty string so .contains will return all airlines etc or any partial matches
            if (customerName ==  null) { customerName = ""; } 

            // the ordinal OR operator || returns the lefthand expression if true, the right hand expression if not
            return madeReservations.Where(r => (r.Flight.FlightCode.Equals(code, StringComparison.OrdinalIgnoreCase) || (r.Flight.FlightCode.Length == code.Length + 1)) &&
                                        (r.Flight.Airline.Contains(airline, StringComparison.OrdinalIgnoreCase)) &&
                                        (r.TravelerName.Contains(customerName, StringComparison.OrdinalIgnoreCase))).ToList();

        }




    }
}
