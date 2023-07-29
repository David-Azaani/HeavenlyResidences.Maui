using System.Net.Http.Headers;
using System.Text;
using HeavenlyResidences.Models;
using Newtonsoft.Json;

namespace HeavenlyResidences.Services;

public class ApiService
{
    public static async Task<bool> RegisterUser(string name, string email, string password, string phone)
    {
        var register = new Register()
        {
            Name = name,
            Email = email,
            Password = password,
            Phone = phone
        };
        try
        {


            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(register);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/users/register", content);

            if (!response.IsSuccessStatusCode) return false;

            return true;

        }

        catch (Exception e)
        {


            return false;

        }


    }

    public static async Task<bool> Login(string email, string password)
    {
        var login = new Login()
        {
            Email = email,
            Password = password
        };
        try
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(login);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/users/login", content);
            if (!response.IsSuccessStatusCode) return false;
            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Token>(jsonResult);
            Preferences.Set("accesstoken", result.AccessToken);
            Preferences.Set("userid", result.UserId);
            Preferences.Set("username", result.UserName);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }

    }
    public static async Task<List<Category>> GetCategories()
    {
        try
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/categories");
            return JsonConvert.DeserializeObject<List<Category>>(response);
        }
        catch (Exception e)
        {
            return new List<Category>();
        }

    }

    public static async Task<List<TrendingProperty>> GetTrendingProperties()
    {
        try
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/Properties/TrendingProperties");
            return JsonConvert.DeserializeObject<List<TrendingProperty>>(response);
        }
        catch (Exception e)
        {
            return new List<TrendingProperty>();
        }

    }

    public static async Task<List<SearchProperty>> FindProperties(string address)
    {
        try
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/Properties/SearchProperties?address=" + address);
            return JsonConvert.DeserializeObject<List<SearchProperty>>(response);
        }
        catch (Exception e)
        {
            return new List<SearchProperty>();
        }

    }

    public static async Task<List<PropertyByCategory>> GetPropertyByCategory(int categoryId = 0)
    {
        try
        {
            if (categoryId != 0)
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
                var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/Properties/PropertyList?categoryId=" + categoryId);
                return JsonConvert.DeserializeObject<List<PropertyByCategory>>(response);
            }

            return new List<PropertyByCategory>();
        }
        catch (Exception e)
        {
            return new List<PropertyByCategory>();
        }

    }

    public static async Task<PropertyDetail> GetPropertyDetail(int propertyId = 0)
    {
        try
        {
            if (propertyId != 0)
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
                var response =
                    await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/Properties/PropertyDetail?id=" +
                                                    propertyId);
                return JsonConvert.DeserializeObject<PropertyDetail>(response);
            }
            return new PropertyDetail();
        }
        catch (Exception e)
        {
            return new PropertyDetail();
        }

    }

    public static async Task<List<BookmarkList>> GetBookmarkList()
    {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
        var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/bookmarks");
        return JsonConvert.DeserializeObject<List<BookmarkList>>(response);
    }

    public static async Task<bool> AddBookmark(AddBookmark addBookmark)
    {
        try
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(addBookmark);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/bookmarks", content);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }
        catch (Exception e)
        {
            return false;
        }

    }

    public static async Task<bool> DeleteBookmark(int bookmarkId)
    {
        try
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpClient.DeleteAsync(AppSettings.ApiUrl + "api/bookmarks/" + bookmarkId);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
        
    }
}

