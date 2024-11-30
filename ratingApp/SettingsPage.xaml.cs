namespace ratingApp;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
        LoadSettings(); // Load settings when the page is opened
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

        // Load user name
        string userName = Preferences.Get("UserName", "");
        UserNameEntry.Text = userName;
    }
}
