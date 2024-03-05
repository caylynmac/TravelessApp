
using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelessApp.Models
{
    class FlightsRepository
    {
        //define list to store flights
        static List<Flight> Flights = new List<Flight>() {
        
            //test data
            new Flight
        {
            FlightCode = "m",
            Airline = "m",
            From = "m",
            To = "m",
            Day = "m",
            Time = "m",
            Cost = "m",
        }
        };


        //import csv data 
        //not working yet

        FlightsRepository()
        {
            string[] lines = File.ReadAllLines("C:cprg211/labs/TravelessApp/Models/flights.csv");

            foreach (string line in lines)
            {

                string[] data = line.Split(',');
                Flight AddFlight = new Flight
                {
                    FlightCode = data[0],
                    Airline = data[1],
                    From = data[2],
                    To = data[3],
                    Day = data[4],
                    Time = data[5],
                    Cost = data[6],
                };

                Flights.Add(AddFlight);
                
            }

        }


            //Find matching flights

            //can't make method public??

            public static List<Flight> FindFlights(string origin, string destination, string day)
            {
                //create list to store matches
                List<Flight> FlightsFound = new List<Flight>();

            foreach (Flight flight in Flights)
            {

                //iterate then copy matches to new list
                if ((flight.From == origin) && (flight.To == destination) && (flight.Day == day))
                {
                    Flight FoundFlight = new Flight
                    {
                        FlightCode = flight.FlightCode,
                        Airline = flight.Airline,
                        From = flight.From,
                        To = flight.To,
                        Day = flight.Day,
                        Time = flight.Time,
                        Cost = flight.Cost
                    };


                    FlightsFound.Add(FoundFlight);
                }
            }
            Console.WriteLine(FlightsFound);
            return FlightsFound;

            }
        }

    }

