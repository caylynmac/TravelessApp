using TravelessApp.Views;


namespace TravelessApp
{
    public partial class AppShell : Shell
    {

        public AppShell()
        {
            InitializeComponent();


            Routing.RegisterRoute("HomePage", typeof(MainPage));
            Routing.RegisterRoute(nameof(FlightsPage), typeof(FlightsPage));
            Routing.RegisterRoute(nameof(SearchReservationsPage), typeof(SearchReservationsPage));

        }

        private void Home_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("HomePage");
        }

        private void Flights_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync(nameof(FlightsPage));
        }

        private void Reservations_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync(nameof(SearchReservationsPage));
        }
    }
    //IconImageSource="{FontImage &#xf4a4;, FontFamily=Ionicons, Size=900, Color=white}
    //IconImageSource="{FontImage &#xf489;, FontFamily=Ionicons, Size=900, Color=white}"
}
