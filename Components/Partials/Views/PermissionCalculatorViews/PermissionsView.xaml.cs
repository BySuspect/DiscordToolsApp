using DiscordToolsApp.Components.Partials.Views.CustomItemViews;

namespace DiscordToolsApp.Components.Partials.Views.PermissionCalculatorViews;

public partial class PermissionsView : ContentView
{
    public List<string> PermissionsList = new List<string>();

    public PermissionsView()
    {
        InitializeComponent();
    }
    public void SetPermissions(List<string> permissions)
    {
        try
        {
            if (permissions is null)
                return;
            foreach (var permission in permissions)
            {
                var checkBox = (CustomCheckBox)FindByName("cb" + permission);
                if (checkBox != null)
                {
                    checkBox.IsChecked = true;
                }
            }
        }
        catch
        {
        }
    }

    private void CustomCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var list = PermissionsList;

        if (e.Value)
        {
            list.Add(((CustomCheckBox)sender).AutomationId);
        }
        else
        {
            list.Remove(((CustomCheckBox)sender).AutomationId);
        }

        EventHandler<PermissionsChangedEventArgs> handler = PermissionsChanged;
        handler?.Invoke(this, new PermissionsChangedEventArgs(PermissionsList, list));

        PermissionsList = list;
    }
    public event EventHandler<PermissionsChangedEventArgs> PermissionsChanged;

}