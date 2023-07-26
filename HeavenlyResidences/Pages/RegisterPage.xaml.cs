using HeavenlyResidences.Services;

namespace HeavenlyResidences.Pages;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
    }


    private async void BtnRegister_OnClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(EnEmail.Text) || string.IsNullOrEmpty(EnFullName.Text)
            || string.IsNullOrEmpty(EnPhone.Text) || string.IsNullOrEmpty(EnPassword.Text))
        {
            await DisplayAlert("", "insert valid data", "Ok");

            await Navigation.PushModalAsync(new LoginPage());
        }

        var response = await ApiService.RegisterUser(EnFullName.Text, EnEmail.Text, EnPassword.Text, EnPhone.Text);
        if (response)
        {
            await DisplayAlert("", "Your account has been created", "Ok");
        }
        else
        {
            await DisplayAlert("", "Oops something went wrong", "Ok");
        }
    }

    private async void TapLogin_OnTapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushModalAsync(new LoginPage());
    }
}