using Newtonsoft.Json;

namespace DiscordToolsApp.Components.Pages;

public partial class TimeStampGeneratorPage : ContentPage
{
    public TimeStampGeneratorPage()
    {
        InitializeComponent();
    }

    private void TimestampChanged(object sender, Models.TimestampModel e)
    {
        Console.WriteLine(JsonConvert.SerializeObject(e));
    }
}

public class MonkeyDataTemplateSelector : DataTemplateSelector
{
    public DataTemplate AmericanMonkey { get; set; }
    public DataTemplate OtherMonkey { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        return ((Monkey)item).Location.Contains("America") ? AmericanMonkey : OtherMonkey;
    }
}

public class Monkey
{
    public string Name { get; set; }
    public string Location { get; set; }
    public string Details { get; set; }
    public string ImageUrl { get; set; }
}
