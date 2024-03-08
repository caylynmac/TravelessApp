// FlightsRepository.cs
//using Java.Lang;
using System.Reflection;

namespace TravelessApp.Models
{
    public class FlightsRepository
    {
        public static List<Flight> Flights = new List<Flight>();

        string flightsFile = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "../../../../../../Models/flights.csv";

        public FlightsRepository()
        {

            ReadFlightsFromCSV(flightsFile);
        }

        private void ReadFlightsFromCSV(string filePath)
        {
            try
            {
                
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
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

        public static List<Flight> FindFlights(string origin, string destination, string dayOfWeek)
        {
            //get airport codes if the input is greater than 3 (assuming a city input)

            if (origin.Length > 3) { origin = GetAirportCode(origin); }
            if (destination.Length > 3) { destination = GetAirportCode(destination); }

            //it will check for matches if a if user inputted the airport code or a city
            return Flights.Where(f => f.From.Equals(origin, StringComparison.OrdinalIgnoreCase) &&
                                       f.To.Equals(destination, StringComparison.OrdinalIgnoreCase) &&
                                       f.Day.Equals(dayOfWeek, StringComparison.OrdinalIgnoreCase)).ToList();

        }

        
        public static List<Flight> FindReservations(string AirlineCode, string AirlineName, string customerName)
        {
   
            return Flights.Where(f => f.FlightCode.Equals(AirlineCode, StringComparison.OrdinalIgnoreCase) &&
                                       f.Airline.Equals(AirlineName, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        //use airports.csv file to convert city name inputted by user to Airport code to match to the From and To properties properly
        //so that the user can type in a city name and find the corresponding airport that contains it in the airport name string

        public static string GetAirportCode(string cityName)
        {
            //string airportsFile = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "../../../../../../Models/airports.csv";
            string airportsFile = @"C:\\Users\\Cayly\\Downloads\\TravelessApp-Seth\\TravelessApp-Seth\\Models\airports.csv";

                string[] lines = File.ReadAllLines(airportsFile);

            string airportCode = cityName;
            

            foreach (string line in lines)
            {
                string[] airportData = line.Split(',');

                //iterate through second column until a match is found, return matching airport that contains city string from first column

                if (airportData[1].Contains(cityName, StringComparison.OrdinalIgnoreCase))
                {
                    airportCode = airportData[0];
                    return airportCode;
                }
            }
            //returns the original input in case the user already entered the airportcode instead of a city name if no matches were found
            return airportCode;
            
        }

        //delete this??
        public static Flight GetFlightByCode(string flightCode)
        {
            return Flights.FirstOrDefault(f => f.FlightCode.Equals(flightCode, StringComparison.OrdinalIgnoreCase));
        }

        //this should be in reservation manager??
        public static Reservation MakeReservation(Flight flight, string travelerName, string citizenship)
        {
            string reservationCode = $"{flight.FlightCode}_{DateTime.Now.Ticks}";
            Reservation reservation = new Reservation(flight, travelerName, citizenship);
            
            return reservation;
        }

    }
}
