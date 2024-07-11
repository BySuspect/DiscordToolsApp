using DiscordToolsApp.Services;

namespace DiscordToolsApp.Components.Partials.Views.CustomItemViews;

public partial class CustomEditorView : ContentView
{
    #region Text Binding
    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text),
        typeof(string),
        typeof(CustomEditorView),
        defaultBindingMode: BindingMode.TwoWay,
        defaultValue: string.Empty
    );
    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set
        {
            SetValue(TextProperty, value);
            OnPropertyChanged(nameof(Text));
        }
    }
    #endregion

    #region Placeholder Binding
    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
        nameof(Placeholder),
        typeof(string),
        typeof(CustomEditorView),
        defaultBindingMode: BindingMode.TwoWay,
        defaultValue: string.Empty
    );
    public string Placeholder
    {
        get { return (string)GetValue(PlaceholderProperty); }
        set
        {
            SetValue(PlaceholderProperty, value);
            OnPropertyChanged(nameof(Placeholder));
        }
    }
    #endregion

    #region MaxLength Binding
    public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(
        nameof(MaxLength),
        typeof(string),
        typeof(CustomEditorView),
        defaultBindingMode: BindingMode.TwoWay,
        defaultValue: string.Empty
    );
    public string MaxLength
    {
        get { return (string)GetValue(MaxLengthProperty); }
        set
        {
            SetValue(MaxLengthProperty, value);
            OnPropertyChanged(nameof(MaxLength));
        }
    }
    #endregion

    #region CornerRadius Binding
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
        nameof(CornerRadius),
        typeof(int),
        typeof(CustomEditorView),
        defaultValue: 15,
        defaultBindingMode: BindingMode.TwoWay
    );
    public int CornerRadius
    {
        get { return (int)GetValue(CornerRadiusProperty); }
        set
        {
            SetValue(CornerRadiusProperty, value);
            OnPropertyChanged(nameof(CornerRadius));
        }
    }
    #endregion

    #region BorderColor Binding
    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
        nameof(BorderColor),
        typeof(Color),
        typeof(CustomEditorView),
        defaultValue: AppThemeColors.BorderColor,
        defaultBindingMode: BindingMode.TwoWay
    );
    public Color BorderColor
    {
        get { return (Color)GetValue(BorderColorProperty); }
        set
        {
            SetValue(BorderColorProperty, value);
            OnPropertyChanged(nameof(BorderColor));
        }
    }
    #endregion

    #region ValidationType Binding
    private ValidationType validationType;
    public ValidationType ValidationType
    {
        get { return validationType; }
        set
        {
            if (validationType != value)
            {
                validationType = value;

                if (value is ValidationType.ColorHex)
                {
                    Input.Behaviors.Clear();
                    Input.Behaviors.Add(new InputBehaviors.ColorHexValidatorBehaviour());
                }
                else if (value is ValidationType.Url)
                {
                    Input.Behaviors.Clear();
                    Input.Behaviors.Add(new InputBehaviors.UrlValidatorBehaviour());
                }
                else if (value is ValidationType.ImageUrl)
                {
                    Input.Behaviors.Clear();
                    Input.Behaviors.Add(new InputBehaviors.ImageUrlValidatorBehaviour());
                }
                else if (value is ValidationType.WebhookUrl)
                {
                    Input.Behaviors.Clear();
                    Input.Behaviors.Add(new InputBehaviors.WebhookUrlValidatorBehaviour());
                }
                else if (value is ValidationType.Numeric)
                {
                    Input.Behaviors.Clear();
                    Input.Behaviors.Add(
                        new CommunityToolkit.Maui.Behaviors.NumericValidationBehavior()
                    );
                }
                else
                {
                    Input.Behaviors.Clear();
                }
            }
        }
    }
    #endregion

    #region HasTitle Binding
    public static readonly BindableProperty HasTitleProperty = BindableProperty.Create(
        nameof(HasTitle),
        typeof(bool),
        typeof(CustomEditorView),
        defaultValue: true,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (CustomEditorView)bindable;
            if ((bool)newValue)
            {
                control.Input.PlaceholderColor = Colors.Transparent;
            }
            else
            {
                control.Input.PlaceholderColor = AppThemeColors.PlaceholderTextColor;
            }
        }
    );
    public bool HasTitle
    {
        get { return (bool)GetValue(HasTitleProperty); }
        set
        {
            SetValue(HasTitleProperty, value);
            OnPropertyChanged(nameof(HasTitle));
        }
    }
    #endregion

    #region HasCharCounterText Binding
    public static readonly BindableProperty HasCharCounterTextProperty = BindableProperty.Create(
        nameof(HasCharCounterText),
        typeof(bool),
        typeof(CustomEditorView),
        defaultValue: true,
        defaultBindingMode: BindingMode.TwoWay
    );
    public bool HasCharCounterText
    {
        get { return (bool)GetValue(HasCharCounterTextProperty); }
        set
        {
            SetValue(HasCharCounterTextProperty, value);
            OnPropertyChanged(nameof(HasCharCounterText));
        }
    }
    #endregion

    #region HorizontalTextAlignment Binding
    public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create(
        nameof(HorizontalTextAlignment),
        typeof(TextAlignment),
        typeof(CustomEditorView),
        defaultValue: TextAlignment.Start,
        defaultBindingMode: BindingMode.TwoWay
    );
    public TextAlignment HorizontalTextAlignment
    {
        get { return (TextAlignment)GetValue(HorizontalTextAlignmentProperty); }
        set
        {
            SetValue(HorizontalTextAlignmentProperty, value);
            OnPropertyChanged(nameof(HorizontalTextAlignment));
        }
    }
    #endregion

    public CustomEditorView()
    {
        InitializeComponent();
        BindingContext = this;

        if (this.Text.Length > 0)
        {
            lblTitle.TextColor = AppThemeColors.TextColor;
            titleView.BorderColor = AppThemeColors.BorderColor;
            titleView.BackgroundColor = AppThemeColors.BackgroundColor;
            titleView.TranslationX = 15;
            titleView.TranslationY = -8;
        }
        else
        {
            lblTitle.TextColor = AppThemeColors.PlaceholderTextColor;
            titleView.BorderColor = Colors.Transparent;
            titleView.BackgroundColor = Colors.Transparent;
            titleView.TranslationX = 0;
            titleView.TranslationY = 13;
        }
    }

    private void Input_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (e.NewTextValue.Length >= Input.MaxLength)
            lblInputLenght.TextColor = Colors.Red;
        else
            lblInputLenght.TextColor = AppThemeColors.TextColor;

        spCharacterCount.Text = e.NewTextValue.Length.ToString();

        if (e.NewTextValue.Length > 0)
        {
            titleView.CancelAnimations();
            lblTitle.TextColor = AppThemeColors.TextColor;
            titleView.BorderColor = AppThemeColors.BorderColor;
            titleView.BackgroundColor = AppThemeColors.BackgroundColor;
            titleView.TranslateTo(15, -9, 150);
        }
        else
        {
            titleView.CancelAnimations();
            lblTitle.TextColor = AppThemeColors.PlaceholderTextColor;
            titleView.BorderColor = Colors.Transparent;
            titleView.BackgroundColor = Colors.Transparent;
            titleView.TranslateTo(0, 13, 150);
        }
    }

    private void titleTapped(object sender, EventArgs e)
    {
        Input.Focus();
    }
}
