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
            LoadFlightNames();
        }

        private void LoadFlightNames()
        {
            List<string> flightNames = FlightsRepository.GetFlightNames();
            FlightPicker.ItemsSource = flightNames;
        }

        private void FindFlights_Clicked(object sender, EventArgs e)
        {
            // Parse "From" and "To" times
            DateTime fromTime, toTime;
            if (!DateTime.TryParseExact(From.Text, "HH:mm", null, System.Globalization.DateTimeStyles.None, out fromTime))
            {
                DisplayAlert("Error", "Invalid 'From' time format. Please use HH:mm format.", "OK");
                return;
            }
            if (!DateTime.TryParseExact(To.Text, "HH:mm", null, System.Globalization.DateTimeStyles.None, out toTime))
            {
                DisplayAlert("Error", "Invalid 'To' time format. Please use HH:mm format.", "OK");
                return;
            }

            string dayOfWeek = Day.Text;

            // Convert times to string for comparison with Flight objects
            string fromTimeString = fromTime.ToString("HH:mm");
            string toTimeString = toTime.ToString("HH:mm");

            // Find flights within the time range
            List<Flight> foundFlights = FlightsRepository.FindFlights(fromTimeString, toTimeString, dayOfWeek);

            if (foundFlights != null && foundFlights.Any())
            {
                FlightPicker.ItemsSource = foundFlights.Select(f => f.FlightCode).ToList();
            }
            else
            {
                DisplayAlert("No Flights Found", "No flights found for the selected criteria.", "OK");
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
