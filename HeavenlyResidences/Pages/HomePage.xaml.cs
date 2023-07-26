namespace HeavenlyResidences.Pages;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();

        var userName =Preferences.Get("username", String.Empty);
        LblUserName.Text = string.IsNullOrEmpty(userName) ?  "Please login again" : $"Hi {userName}";
    }
}