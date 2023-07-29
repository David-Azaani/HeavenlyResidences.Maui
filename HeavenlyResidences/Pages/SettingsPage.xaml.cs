namespace HeavenlyResidences.Pages;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
    }

    private void TapLogout_OnTapped(object sender, TappedEventArgs e)
    {
        Preferences.Set("accesstoken", String.Empty);
        Application.Current.MainPage = new LoginPage();
    }

    private async void TapFaq_OnTapped(object sender, TappedEventArgs e)
    {
        await DisplayAlert("", "Faq", "Ok");
    }

    private async void TapPrivacy_OnTapped(object sender, TappedEventArgs e)
    {
        await DisplayAlert("", "Privacy", "Ok");
    }

    private async void TapAbout_OnTapped(object sender, TappedEventArgs e)
    {
        await DisplayAlert("", "About", "Ok");
    }
}