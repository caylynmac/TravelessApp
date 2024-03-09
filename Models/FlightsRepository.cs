// FlightsRepository.cs
//using Java.Lang;
using System.Reflection;

namespace TravelessApp.Models
{
    public class FlightsRepository
    {
        public static List<Flight> Flights;

        string FLIGHTSFILE = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "../../../../../../Models/flights.csv";

        public FlightsRepository()
        {
            Flights = new List<Flight>();
            ReadFlightsFromCSV(FLIGHTSFILE);
        }

        private void ReadFlightsFromCSV(string filePath)
        {
            try
            {
                
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] data = line.Split(',');

                    //make time 24h format
                    if (data[5].Length != 5)
                    {
                        data[5] = "0" + data[5];
                    }

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
            //get airport codes if the input is greater than 3 (assuming a city input) or nothing was inputted (text = default)
            //it will check for matches if a if user inputted the airport code or a city
            if ((origin != null) && (origin.Length > 3)) { origin = GetAirportCode(origin); }
            if ((destination != null) && destination.Length > 3) { destination = GetAirportCode(destination); }

            //if any of the input fields were not filled in, reset them to be length 2 or length 5 for day
            //so that all flight objects with any origin and/or destination and/or day can be returned
            if (string.IsNullOrEmpty(origin)){ origin = "xx"; } //length 2 so it becomes length 3 in the ordinal clause
            if (string.IsNullOrEmpty(destination)) { destination = "xx"; } //the input string becomes empty if a value is entered then deleted
            if (dayOfWeek == null) { dayOfWeek = ""; } //make null an empty string so .contains will return all days or any partial matches

            // the ordinal OR operator || returns the lefthand expression if true, the right hand expression if not
            return Flights.Where(f => (f.From.Equals(origin, StringComparison.OrdinalIgnoreCase) || f.From.Length == origin.Length + 1) &&
                                        (f.To.Equals(destination, StringComparison.OrdinalIgnoreCase) || f.To.Length == destination.Length + 1) &&
                                        (f.Day.Contains(dayOfWeek, StringComparison.OrdinalIgnoreCase))).ToList();
        }

        //use airports.csv file to convert city name inputted by user to Airport code to match to the From and To properties properly
        //so that the user can type in a city name and find the corresponding airport that contains it in the airport name string

        public static string GetAirportCode(string cityName)
        {
            string AIRPORTSFILE = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "../../../../../../Models/airports.csv";

            string[] lines = File.ReadAllLines(AIRPORTSFILE);

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
        public static Flight GetFlightByCode(string flightCode)
        {
            return Flights.FirstOrDefault(f => f.FlightCode.Equals(flightCode, StringComparison.OrdinalIgnoreCase));
        }

    }
}
