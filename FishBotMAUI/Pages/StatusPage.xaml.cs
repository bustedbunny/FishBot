using FishBotMAUI.ViewModel;

namespace FishBotMAUI.Pages
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