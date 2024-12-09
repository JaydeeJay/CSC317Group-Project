namespace ratingApp;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
        LoadSettings(); // Load settings when the page is opened

        // Handle theme changes dynamically
        ThemePicker.SelectedIndexChanged += OnThemeSelected;
    }

    private async void OnChangeProfilePicture(object sender, EventArgs e)
    {
        // Implement profile picture picker logic (file picker or camera)
        var fileResult = await FilePicker.Default.PickAsync(new PickOptions
        {
            FileTypes = FilePickerFileType.Images,
            PickerTitle = "Select a Profile Picture"
        });

        if (fileResult != null)
        {
            ProfileImage.Source = ImageSource.FromFile(fileResult.FullPath);

            // Save the profile picture path
            Preferences.Set("ProfilePicturePath", fileResult.FullPath);
        }
    }

    private void OnSaveSettings(object sender, EventArgs e)
    {
        // Save selected theme
        string selectedTheme = ThemePicker.SelectedItem?.ToString();
        if (!string.IsNullOrEmpty(selectedTheme))
        {
            Preferences.Set("AppTheme", selectedTheme);
        }

        // Save user name
        string userName = UserNameEntry.Text;
        Preferences.Set("UserName", userName);

        DisplayAlert("Settings Saved", "Your preferences have been saved!", "OK");
    }

    private void LoadSettings()
    {
        // Load profile picture
        string profilePicturePath = Preferences.Get("ProfilePicturePath", null);
        if (!string.IsNullOrEmpty(profilePicturePath))
        {
            ProfileImage.Source = ImageSource.FromFile(profilePicturePath);
        }

        // Load theme
        string appTheme = Preferences.Get("AppTheme", "Light");
        ThemePicker.SelectedItem = appTheme;
        ApplyTheme(appTheme); // Apply the saved theme

        // Load user name
        string userName = Preferences.Get("UserName", "");
        UserNameEntry.Text = userName;
    }

    private void OnThemeSelected(object sender, EventArgs e)
    {
        // Update gradient background based on the selected theme
        string selectedTheme = ThemePicker.SelectedItem as string;
        ApplyTheme(selectedTheme);
    }

    private void ApplyTheme(string theme)
    {
        switch (theme)
        {
            case "Light":
                SetGradientBackground("#BD2FFF", "#000000");
                break;

            case "Dark":
                SetGradientBackground("#000000", "#2E2E2E");
                break;

            case "Red":
                SetGradientBackground("#FF0000", "#000000");
                break;

            default:
                SetGradientBackground("#BD2FFF", "#000000"); // Default gradient
                break;
        }
    }

    private void SetGradientBackground(string startColor, string endColor)
    {
        this.Background = new LinearGradientBrush
        {
            EndPoint = new Point(0, 1),
            GradientStops = new GradientStopCollection
            {
                new GradientStop { Color = Color.FromArgb(startColor), Offset = 0.1f },
                new GradientStop { Color = Color.FromArgb(endColor), Offset = 1.0f }
            }
        };
    }
}

