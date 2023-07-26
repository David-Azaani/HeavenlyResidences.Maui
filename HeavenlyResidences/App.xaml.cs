using HeavenlyResidences.Pages;
using HeavenlyResidences.Services;

namespace HeavenlyResidences
{
    public partial class App : Application
    {
        
        

        public App()
        {
            InitializeComponent();

            //Choosing Main Page
            MainPage = new RegisterPage();
        }
    }
}