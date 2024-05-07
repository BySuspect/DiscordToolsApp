using System.Security.AccessControl;
using DiscordToolsApp.Components.Models;
using DiscordToolsApp.Components.Partials.Views.CommonViews;

namespace DiscordToolsApp.Components.Pages;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void MainPagCustomButtonView_Clicked(object sender, ClickedEventArgs e)
    {
        var clicked = ((MainPagCustomButtonView)sender);
        clicked.IsEnabled = false;

        switch (clicked.PageType)
        {
            case MainPageButtonsPageTypeModel.TimeStampGeneratorPage:
                await Shell.Current.GoToAsync("//TimeStampGeneratorPage", true);
                break;

            case MainPageButtonsPageTypeModel.UserLookupPage:
                await Shell.Current.GoToAsync("//UserLookupPage", true);
                break;

            case MainPageButtonsPageTypeModel.Empty:
                break;

            default:
                await Shell.Current.GoToAsync("//MainPage", true);
                break;
        }

        clicked.IsEnabled = true;
    }
}
