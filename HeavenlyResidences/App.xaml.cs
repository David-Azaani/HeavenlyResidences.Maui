using HeavenlyResidences.Pages;
using HeavenlyResidences.Services;

namespace HeavenlyResidences
{
    public partial class App : Application
    {
        
        

        public App()
        {
            InitializeComponent();


            var accessToken =Preferences.Get("accesstoken",String.Empty);
            if (String.IsNullOrEmpty(accessToken))
            {
                MainPage = new LoginPage();
            }
            else
            {
                  MainPage = new CustomTabbedPage();
            }

            //Choosing Main Page
            
        }
    }
}