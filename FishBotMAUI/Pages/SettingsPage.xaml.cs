using FishBotMAUI.ViewModel;

namespace FishBotMAUI.Pages;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}