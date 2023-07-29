using HeavenlyResidences.Models;
using HeavenlyResidences.Services;

namespace HeavenlyResidences.Pages;

public partial class SearchPage : ContentPage
{
    public SearchPage()
    {
        InitializeComponent();
    }

    private async void ImgBackButton_OnClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async void SbProperty_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (e.NewTextValue==null) return;

      var result = await ApiService.FindProperties(e.NewTextValue);

      CvSearch.ItemsSource = result;

    }

    private async void CvSearch_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as SearchProperty;

        if (currentSelection == null) return;

        await Navigation.PushModalAsync(new PropertyDetailPage(currentSelection.Id));
        ((CollectionView)sender).SelectedItem = null;
    }
}