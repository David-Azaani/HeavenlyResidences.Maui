using HeavenlyResidences.Models;
using HeavenlyResidences.Services;

namespace HeavenlyResidences.Pages;

public partial class BookmarksPage : ContentPage
{
    public BookmarksPage()
    {
        InitializeComponent();
        GetPropertyList().WaitAsync(new CancellationToken());
    }

    private async Task GetPropertyList()
    {
        var bookmarkList = await ApiService.GetBookmarkList();
        CvProperties.ItemsSource = bookmarkList;

    }

    protected override bool OnBackButtonPressed()
    {
        return base.OnBackButtonPressed();
    }

    protected override async void OnAppearing()
    {
       await GetPropertyList();
        base.OnAppearing();
    }
    
    private async void CvProperties_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as BookmarkList;

        if (currentSelection == null) return;

        await Navigation.PushModalAsync(new PropertyDetailPage(currentSelection.PropertyId));
        ((CollectionView)sender).SelectedItem = null;

    }
}