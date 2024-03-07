using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TravelessApp.Models;

namespace TravelessApp.Views;

public partial class SearchReservationsPage : ContentPage
{
	public SearchReservationsPage()
	{
		InitializeComponent();
    	}


    private void FindReservation_Clicked(object sender, EventArgs e)
    	{

        string code = FlightCode.Text;
        string airline = Airline.Text;
        string name = Name.Text;


        // Find flights with specified parameters
        List<Flight> foundReservations = FlightsRepository.FindReservations(code, airline, name);

        if (foundReservations != null && foundReservations.Any())
        {
            reservationPicker.ItemsSource = foundReservations;
        }
        else
        {
            DisplayAlert("No Flights Found", "No flights found for the selected criteria.", "OK");
        }
    	}

}
