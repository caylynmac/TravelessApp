using TravelessApp.Models;

namespace TravelessApp.Views;

public partial class FlightsPage : ContentPage
{
	public FlightsPage()
	{
        InitializeComponent();

	}

    private void FindFlights_Clicked(object sender, EventArgs e)
    {

        //test
        List<Flight> flight = FlightsRepository.FindFlights("m", "m", "m");

        if (flight[0] != null)
        {
            //setting labels and entries 
            

        }

    }
}