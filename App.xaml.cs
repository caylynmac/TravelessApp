
namespace TravelessApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();


        }

        //set initial window height and position
        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);

            window.Height = 900;
            window.X = 0;
            window.Y = 0;   
            return window;
        }
    }
}
