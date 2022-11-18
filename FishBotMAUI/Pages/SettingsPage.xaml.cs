using FishBotMAUI.ViewModel;

namespace FishBotMAUI.Pages;

public partial class SettingsPage
{
	public SettingsPage(SettingsPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}