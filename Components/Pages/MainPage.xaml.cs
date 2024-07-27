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
        if (!Preferences.Get("PrivacyPolicyV1Accepted", false))
            ApplicationService.ShowPopup(new PrivacyPolicyPopup());

        ApplicationService.ShowPopup(
            new NoticePopup(
                "In light of Google's new policies, I�ve decided to remove my apps from the Play Store. I will make them fully open source and share updates as APKs on GitHub. If you'd like to stay informed about developments, please join my Discord server."
            )
        );

        base.OnAppearing();
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
