using System.Security.AccessControl;

using DiscordToolsApp.Components.Models;
using DiscordToolsApp.Components.Partials.Views.MainPageViews;

namespace DiscordToolsApp.Components.Pages;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        ApplicationService.ActivePage = "MainPage";
    }

    private async void MainPagCustomButtonView_Clicked(object sender, ClickedEventArgs e)
    {
        var clicked = ((MainPagCustomButtonView)sender);
        clicked.IsEnabled = false;

        switch (clicked.PageType)
        {
            case MainPageButtonsPageTypeModel.TimeStampGeneratorPage:
                ApplicationService.ActivePage = "TimeStampGeneratorPage";
                await Shell.Current.GoToAsync("//TimeStampGeneratorPage", true);
                break;

            case MainPageButtonsPageTypeModel.UserLookupPage:
                ApplicationService.ActivePage = "UserLookupPage";
                await Shell.Current.GoToAsync("//UserLookupPage", true);
                break;

            case MainPageButtonsPageTypeModel.InviteLookupPage:
                ApplicationService.ActivePage = "InviteLookupPage";
                await Shell.Current.GoToAsync("//InviteLookupPage", true);
                break;
            case MainPageButtonsPageTypeModel.TextToEmojiPage:
                ApplicationService.ActivePage = "TextToEmojiPage";
                await Shell.Current.GoToAsync("//TextToEmojiPage", true);
                break;

            case MainPageButtonsPageTypeModel.Empty:
                break;

            default:
                ApplicationService.ActivePage = "MainPage";
                await Shell.Current.GoToAsync("//MainPage", true);
                break;
        }

        clicked.IsEnabled = true;
    }
}
