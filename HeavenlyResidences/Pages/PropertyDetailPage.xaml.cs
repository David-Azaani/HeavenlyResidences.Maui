using HeavenlyResidences.Models;
using HeavenlyResidences.Services;

namespace HeavenlyResidences.Pages;

public partial class PropertyDetailPage : ContentPage
{
    private int _propertyId;
    private int _bookmarkId = 0;
    private string _phoneNumber;
    private bool _isBookmarkEnabled;
    private bool _istapped = false;
    public PropertyDetailPage(int propertyId)
    {
        _propertyId = propertyId;
        InitializeComponent();

        GetPropertyDetail(propertyId);
    }

    private async void GetPropertyDetail(int propertyId)
    {
        var detail = await ApiService.GetPropertyDetail(propertyId);
        if (detail != null)
        {
            LblPrice.Text = detail.Price.ToString("C");
            LblAddress.Text = detail.Address;
            LblDescription.Text = detail.Detail;
            ImgProperty.Source = detail.FullImageUrl;
            _phoneNumber = detail.Phone;

            if (detail.Bookmark == null)
            {
                ImgBookmarkBtn.Source = "bookmark_empty_icon";
                _isBookmarkEnabled = false;
            }
            else
            {
                ImgBookmarkBtn.Source = detail.Bookmark.Status ? "bookmark_fill_icon" : "bookmark_empty_icon";
                
                _bookmarkId = detail.Bookmark.Id;
                _isBookmarkEnabled = true;
            }
        }

    }

    private async void TapCall_OnTapped(object sender, TappedEventArgs e)
    {

        if (PhoneDialer.Default.IsSupported)
            PhoneDialer.Default.Open(_phoneNumber);
        //PhoneDialer.Default.Open("000-000-0000");
        //if (!string.IsNullOrEmpty(_phoneNumber))
        //{
        //    PhoneDialer.Open(_phoneNumber);
        //}
        //else
        //{
        //    await DisplayAlert("", "No phone number is registered !!!", "Ok");
        //}

    }

    private async void TapMessage_OnTapped(object sender, TappedEventArgs e)
    {


        if (Sms.Default.IsComposeSupported)
        {
            // string[] recipients = new[] { "000-000-0000" };
            string text = "Hello, I'm interested in buying your property.";

            var message = new SmsMessage(text, _phoneNumber);
            //var message = new SmsMessage(text, recipients);

            await Sms.Default.ComposeAsync(message);
        }

        //if (!string.IsNullOrEmpty(_phoneNumber))
        //{
        //    var message = new SmsMessage("Hi ! I am interested in your property", _phoneNumber);
        //     Sms.ComposeAsync(message);
        //}
        //else
        //{
        //     DisplayAlert("", "No phone number is registered !!!", "Ok");
        //}
    }

    private async void ImgBackBtn_OnClicked(object sender, EventArgs e)
    {
        if (_istapped)
            return;

          //var pageA = Application.Current.MainPage.Navigation.NavigationStack.FirstOrDefault();
       

        _istapped = true;

        await Navigation.PopModalAsync(true);

        _istapped = false;


        //await Navigation.PopModalAsync();
    }

    private async void ImgBookmarkBtn_OnClicked(object sender, EventArgs e)
    {
        if (_istapped)
            return;

        _istapped = true;

        if (_isBookmarkEnabled == false)
        {
            var bookmark = new AddBookmark()
            {
                User_Id = Preferences.Get("userid", 0),
                PropertyId = _propertyId,

            };
            var res = await ApiService.AddBookmark(bookmark);

            if (res)
            {
                ImgBookmarkBtn.Source = "bookmark_fill_icon";
                _isBookmarkEnabled = true;

            }
        }
        else
        {
            if (_bookmarkId != 0)
            {
                var res = await ApiService.DeleteBookmark(_bookmarkId);
                if (res)
                {
                    ImgBookmarkBtn.Source = "bookmark_empty_icon";
                    _isBookmarkEnabled = false;

                }
            }

        }

        _istapped = false;




        
    }
}