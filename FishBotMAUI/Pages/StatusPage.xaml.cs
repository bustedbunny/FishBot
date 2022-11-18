using FishBotMAUI.Services.Logging;
using FishBotMAUI.ViewModel;

namespace FishBotMAUI
{
    public partial class StatusPage : ContentPage
    {
        public StatusPage(StatusPageViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = viewModel;
        }
    }
}