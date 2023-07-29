using HeavenlyResidences.Models;
using HeavenlyResidences.Services;

namespace HeavenlyResidences.Pages;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();

        var userName =Preferences.Get("username", String.Empty);
        LblUserName.Text = string.IsNullOrEmpty(userName) ?  "Please login again" : $"Hi {userName}";

        GetCategories();
        GetTrendingProperties();


    }

    private async void GetTrendingProperties()
    {
        var trendingProperties = await ApiService.GetTrendingProperties();
        CvTopPicks.ItemsSource = trendingProperties;
        
    }

    private async void GetCategories()
    {
        var categoryList = await ApiService.GetCategories();
        CvCategories.ItemsSource = categoryList;
    }

    private async void CvCategories_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        
        var currentSelection = e.CurrentSelection.FirstOrDefault() as Category;

        if (currentSelection == null) return;

       await Navigation.PushAsync(new PropertiesListPage(currentSelection.Id, currentSelection.Name));
       ((CollectionView)sender).SelectedItem = null;
    }

    private async void CvTopPicks_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as TrendingProperty;

        if (currentSelection == null) return;

        await Navigation.PushModalAsync(new PropertyDetailPage(currentSelection.Id));
        ((CollectionView)sender).SelectedItem = null;
    }


    //private void TapGestureRecognizer_OnTapped(object sender, TappedEventArgs e)
    //{
    //    throw new NotImplementedException();
    //}

    //private async void TapTopPick_OnTapped(object sender, TappedEventArgs e)
    //{
    //    var currentSelection = e.Parameter as TrendingProperty;

    //    if (currentSelection == null) return;

    //    await Navigation.PushModalAsync(new PropertyDetailPage(currentSelection.Id));
    //    ((CollectionView)sender).SelectedItem = null;


        
    //}
    private async void TapSearch_OnTapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushModalAsync(new SearchPage());
    }
}