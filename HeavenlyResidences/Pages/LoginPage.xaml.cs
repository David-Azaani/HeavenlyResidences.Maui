using HeavenlyResidences.Services;

namespace HeavenlyResidences.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private async void JoinNow_OnTapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushModalAsync(new RegisterPage());
    }

    private async void BtnLogin_OnClicked(object sender, EventArgs e)
    {

        if (string.IsNullOrEmpty(EnEmail.Text) || string.IsNullOrEmpty(EnPassword.Text))
        {
            await DisplayAlert("", "insert valid data", "Ok");

           
        }

        var response = await ApiService.Login(EnEmail.Text,  EnPassword.Text);
        if (response)
        {
            Application.Current.MainPage = new HomePage();
        }
        else
        {
            await DisplayAlert("", "Oops something went wrong", "Ok");
        }
    }
}