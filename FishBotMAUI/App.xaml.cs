using System.Diagnostics;

namespace FishBotMAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();


        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);

#if WINDOWS
            window.Activated += OnWindowActivation;
            window.Deactivated += OnWindowDeactivation;
#endif

            window.Destroying += OnWindowDestroying;

            return window;
        }

        private void OnWindowDestroying(object sender, EventArgs e)
        {
            Debug.WriteLine("Kek");
        }
#if WINDOWS

        private const string WindowXPosition = nameof(WindowXPosition);
        private const string WindowYPosition = nameof(WindowYPosition);
        private const string WindowHeight = nameof(WindowHeight);
        private const string WindowWidth = nameof(WindowWidth);
        private void OnWindowDeactivation(object sender, EventArgs e)
        {
            var window = sender as Window;
            var prefs = Preferences.Default;

            prefs.Set(WindowXPosition, window.X);
            prefs.Set(WindowYPosition, window.Y);

            prefs.Set(WindowHeight, window.Height);
            prefs.Set(WindowWidth, window.Width);
        }

        private async void OnWindowActivation(object sender, EventArgs e)
        {



            var window = sender as Window;
            var prefs = Preferences.Default;

            // Window size

            const int DefaultWidth = 640;
            const int DefaultHeight = 480;


            window.Height = prefs.Get<double>(WindowHeight, DefaultHeight);
            window.Width = prefs.Get<double>(WindowWidth, DefaultWidth);

            // give it some time to complete window resizing task.
            await window.Dispatcher.DispatchAsync(() => { });

            // Window position

            if (prefs.ContainsKey(WindowXPosition) && prefs.ContainsKey(WindowYPosition))
            {
                window.X = prefs.Get<double>(WindowXPosition, default);
                window.Y = prefs.Get<double>(WindowYPosition, default);
            }
            else
            {
                var disp = DeviceDisplay.Current.MainDisplayInfo;

                // move to screen center
                window.X = (disp.Width / disp.Density - window.Width) / 2;
                window.Y = (disp.Height / disp.Density - window.Height) / 2;
            }
        }
#endif
    }
}