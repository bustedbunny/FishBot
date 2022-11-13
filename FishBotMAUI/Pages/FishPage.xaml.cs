using FishBot;
using FishBotMAUI.ViewModel;

namespace FishBotMAUI.Pages;

public partial class FishPage : ContentPage
{
    public FishPage(FishPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;


    }
}