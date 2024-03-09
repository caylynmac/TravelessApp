//using Intents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelessApp.Models
{
    public class Reservation
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

        public override string ToString()
        {
            return String.Format("{0,-10}{1,-25}{2,-10}{3, -20}{4,-20}{5,-20}${6, -20}{7,-20}{8,-20}", 
                ReservationCode, TravelerName, Citizenship, Flight.FlightCode, Flight.Day, Flight.Time, Flight.Cost, "Origin: " + Flight.From, "Destination: " + Flight.To); // {index, alignment}

        }
    }
}
