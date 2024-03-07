using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TravelessApp.Models;

namespace TravelessApp.Views
{
    public partial class FlightsPage : ContentPage
    {
        public FlightsPage()
        {
            InitializeComponent();
            new FlightsRepository(); //load csv data
            LoadFlightNames();
        }

        //load all flights into picker dropdown before user search
        private void LoadFlightNames()
        {
            
            FlightPicker.ItemsSource = FlightsRepository.Flights;
        }

        private void FindFlights_Clicked(object sender, EventArgs e)
        {

            string dayOfWeek = Day.Text;
            string origin = From.Text;
            string destination = To.Text;


            // Find flights within the time range
            List<Flight> foundFlights = FlightsRepository.FindFlights(origin, destination, dayOfWeek);

            if (foundFlights != null && foundFlights.Any())
            {
                FlightPicker.ItemsSource = foundFlights; 
            }
            else
            {
                DisplayAlert("No Flights Found", "No flights found for the selected criteria.", "OK");
            }
        }

        private void Flight_selected(object sender, EventArgs e)
        {
            //cast sender to picker object then selectedItem to Flight object
            var picker = (Picker)sender;
            Flight f = (Flight)picker.SelectedItem;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                FlightCode.Text = f.FlightCode;
                Airline.Text = f.Airline;
                Day.Text = f.Day;
                Time.Text = f.Time;
                Cost.Text = "$" + f.Cost;
            }
            
        }

        private void ReserveButton_Clicked(object sender, EventArgs e)
        {
            if (FlightPicker.SelectedItem == null)
            {
                DisplayAlert("Error", "Please select a flight.", "OK");
                return;
            }

            if (string.IsNullOrEmpty(Name.Text))
            {
                DisplayAlert("Error", "Please enter your name.", "OK");
                return;
            }

            if (string.IsNullOrEmpty(Citizenship.Text))
            {
                DisplayAlert("Error", "Please enter your citizenship.", "OK");
                return;
            }

            string flightCode = (string)FlightPicker.SelectedItem;
            Flight selectedFlight = FlightsRepository.GetFlightByCode(flightCode);

            if (selectedFlight != null)
            {
                // Generate a reservation code
                string reservationCode = $"{selectedFlight.FlightCode}_{DateTime.Now.Ticks}";

                // Create a reservation
                Reservation reservation = FlightsRepository.MakeReservation(selectedFlight, Name.Text, Citizenship.Text);

                // Display reservation code
                ReservationCode.Text = reservationCode;
            }
            else
            {
                DisplayAlert("Error", "Failed to make reservation. Please try again.", "OK");
            }
        }
    }
}
