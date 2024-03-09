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

            //only call flights repo if this is the first time the page as been opened (count =1) (navigation stack initialized in app shell)

                new FlightsRepository(); //to load csv data and access list of flights
                new ReservationManager(); //to add to list of reservations
                LoadFlightNames();
            
        }

        //load all flights from flightsrepositroy into picker dropdown before user search
        private void LoadFlightNames()
        {
            
            FlightPicker.ItemsSource = FlightsRepository.Flights;
        }

        private void FindFlights_Clicked(object sender, EventArgs e)
        {

            string dayOfWeek = Day.Text;
            string origin = From.Text;
            string destination = To.Text;


            // Find matching flights
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
                ReservationDay.Text = f.Day;
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

            
            Flight f = FlightPicker.SelectedItem as Flight;
            Flight selectedFlight = FlightsRepository.GetFlightByCode(f.FlightCode);

            if (selectedFlight != null)
            {
                //this will make a reservation and add it to the list
                Reservation reservation = ReservationManager.MakeReservation(selectedFlight, Name.Text, Citizenship.Text);

                //display reservation code from the reservation made
                ReservationCode.Text = "Your reservation code is: " + reservation.ReservationCode;
            }
            else
            {
                DisplayAlert("Error","","");
            }

            //reset picker to null so new found flights aren't appended in subsequent searches

        }
    }
}
