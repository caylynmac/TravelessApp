using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TravelessApp.Models;
//using UIKit;

namespace TravelessApp.Views;

public partial class SearchReservationsPage : ContentPage
{
	public SearchReservationsPage()
	{
		InitializeComponent();
        new ReservationManager();
        reservationPicker.ItemsSource = ReservationManager.madeReservations;
    }

    
    //will search reservationmanager's list of reservations made, return list of matches to display in the dropdown
    private void FindReservation_Clicked(object sender, EventArgs e)
    {

        string code = FlightCode.Text;
        string airline = Airline.Text;
        string name = Name.Text;
        
        //matching reservations
        List<Reservation> foundReservations = ReservationManager.FindReservations(code, airline, name);

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

