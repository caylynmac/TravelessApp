// FlightsRepository.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TravelessApp.Models
{
    public class FlightsRepository
    {
        public static List<Flight> Flights = new List<Flight>();

        public FlightsRepository()
        {
            ReadFlightsFromCSV("C:\\Users\\contr\\OneDrive\\Desktop\\C# assignments\\TravelessApp\\Models\\flights.csv");
        }

        private void ReadFlightsFromCSV(string fileName)
        {
            try
            {
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines.Skip(1)) // Skip header line
                {
                    string[] data = line.Split(',');
                    Flight addFlight = new Flight
                    {
                        FlightCode = data[0],
                        Airline = data[1],
                        From = data[2],
                        To = data[3],
                        Day = data[4],
                        Time = data[5],
                        Cost = data[6]
                    };

                    Flights.Add(addFlight);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading CSV file: {ex.Message}");
            }
        }

        public static List<string> GetFlightNames()
        {
            return Flights.Select(f => f.FlightCode).ToList();
        }

        public static List<Flight> FindFlights(string origin, string destination, string dayOfWeek)
        {
            return Flights.Where(f => f.From.Equals(origin, StringComparison.OrdinalIgnoreCase) &&
                                       f.To.Equals(destination, StringComparison.OrdinalIgnoreCase) &&
                                       f.Day.Equals(dayOfWeek, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public static Flight GetFlightByCode(string flightCode)
        {
            return Flights.FirstOrDefault(f => f.FlightCode.Equals(flightCode, StringComparison.OrdinalIgnoreCase));
        }

        public static Reservation MakeReservation(Flight flight, string travelerName, string citizenship)
        {
            string reservationCode = $"{flight.FlightCode}_{DateTime.Now.Ticks}";
            Reservation reservation = new Reservation(reservationCode, flight, travelerName, citizenship);
            
            return reservation;
        }
    }
}
