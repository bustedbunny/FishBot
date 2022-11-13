using FishBotMAUI.Pages;

namespace FishBotMAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(FishPage), typeof(FishPage));
        }
    }
}