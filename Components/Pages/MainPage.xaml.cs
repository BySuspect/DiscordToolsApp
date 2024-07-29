using DiscordToolsApp.Components.Models;
using DiscordToolsApp.Components.Partials.Views.MainPageViews;
using DiscordToolsApp.Components.Popups.Common;
using DiscordToolsApp.Components.Popups.Feedback;

namespace DiscordToolsApp.Components.Pages;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        ApplicationService.ActivePage = "MainPage";

#if DEBUG
        btnTest.IsVisible = true;
#endif
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        Task.Run(AppVersionCheckService.CheckVersion);
    }

    private void MainPagCustomButtonView_Clicked(object sender, ClickedEventArgs e)
    {
        var clicked = ((MainPagCustomButtonView)sender);
        clicked.IsEnabled = false;

        switch (clicked.PageType)
        {
            case MainPageButtonsPageTypeModel.TimeStampGeneratorPage:
                ApplicationService.ActivePage = "TimeStampGeneratorPage";
                Shell.Current.GoToAsync("TimeStampGeneratorPage", true);
                break;

            case MainPageButtonsPageTypeModel.UserLookupPage:
                ApplicationService.ActivePage = "UserLookupPage";
                Shell.Current.GoToAsync("UserLookupPage", true);
                break;

            case MainPageButtonsPageTypeModel.InviteLookupPage:
                ApplicationService.ActivePage = "InviteLookupPage";
                Shell.Current.GoToAsync("InviteLookupPage", true);
                break;

            case MainPageButtonsPageTypeModel.TextToEmojiPage:
                ApplicationService.ActivePage = "TextToEmojiPage";
                Shell.Current.GoToAsync("TextToEmojiPage", true);
                break;

            case MainPageButtonsPageTypeModel.Custom:
#if !DEBUG
                if (Preferences.Get("toolSuggestDate", DateTime.MinValue).Date == DateTime.Now.Date)
                    ApplicationService.ShowCustomAlert(
                        "Warning!",
                        "You can send Daily 1 Suggestion.",
                        "Ok"
                    );
                else
#endif
                ApplicationService.ShowPopup(new SuggestToolIdePopup());
                break;

            case MainPageButtonsPageTypeModel.WebhookSendPage:
                ApplicationService.ActivePage = "WebhookSendPage";
                Shell.Current.GoToAsync("WebhookSendPage", true);
                break;

            case MainPageButtonsPageTypeModel.PermissionCalculatorPage:
                ApplicationService.ActivePage = "PermissionCalculatorPage";
                Shell.Current.GoToAsync("PermissionCalculatorPage", true);
                break;

            case MainPageButtonsPageTypeModel.Empty:
                break;

            default:
                ApplicationService.ActivePage = "MainPage";
                Shell.Current.GoToAsync("MainPage", true);
                break;
        }

        clicked.IsEnabled = true;
    }

    private void btnTest_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("TestPage");
    }
}
