using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Make Reservation
// When a travel agent selects a flight from the list, the text fields are populated with the selected
// flight code, airline, day, time, and cost. The travel agent enters the traveler’s full name and
// citizenship. The flight code, airline, day, time, and cost cannot be edited. An error message
// is displayed if:
// • A reservation is to be made but no flight is selected
// • The name field is empty
// • The citizenship field is empty

// The makeReservation method receives as its input arguments: a Flight object, the traveler's
// name, and citizenship. An exception is thrown if the flight is completely booked, or the
// flight is null, or the name is empty/null, or the citizenship is empty/null. If there are no
// exceptions thrown, a Reservation object is created, saved to the binary file, and returned by
// the method.

namespace TravelessApp.Models
{
    internal class Reservation
    {
        // Properties
        public string ReservationCode { get; } // Unique reservation code
        public Flight Flight { get; } // Associated flight
        public string TravelerName { get; } // Name of the traveler
        public string Citizenship { get; } // Citizenship of the traveler
        public bool IsActive { get; private set; } // Indicates if reservation is active

        // Constructor
        public Reservation(Flight flight, string travelerName, string citizenship)
        {
            // Check for null flight
            Flight = flight ?? throw new ArgumentNullException(nameof(flight), "Flight cannot be null");

            // Check for empty traveler name
            TravelerName = !string.IsNullOrWhiteSpace(travelerName) ? travelerName : throw new ArgumentNullException(nameof(travelerName), "Traveler's name cannot be empty");

            // Check for empty citizenship
            Citizenship = !string.IsNullOrWhiteSpace(citizenship) ? citizenship : throw new ArgumentNullException(nameof(citizenship), "Citizenship cannot be empty");

            // Generate a unique reservation code
            ReservationCode = GenerateReservationCode();

            // By default, reservation is active
            IsActive = true;
        }

        // Method to create a new reservation
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

            // Create and return a new reservation
            return new Reservation(flight, travelerName, citizenship);
        }

        // Method to generate a unique reservation code
        private string GenerateReservationCode()
        {
            // Generate a random reservation code using the format LDDDD
            Random random = new Random();
            char letter = (char)('A' + random.Next(26)); // Random letter
            int number = random.Next(10000); // Random number
            return $"{letter}{number:D4}"; // Format as letter followed by 4-digit number
        }

        // Method to set the reservation as inactive
        public void SetInactive()
        {
            IsActive = false;
            // Here you can implement logic to persist the reservation as inactive
        }
    }
}
