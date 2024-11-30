namespace ratingApp;

public partial class RatingPage : ContentPage
{
    public RatingPage()
    {
        InitializeComponent();
    }

    private async void OnUploadPosterClicked(object sender, EventArgs e)
    {
        // Open file picker for image selection
        var fileResult = await FilePicker.Default.PickAsync(new PickOptions
        {
            FileTypes = FilePickerFileType.Images,
            PickerTitle = "Select a Media Poster"
        });

        if (fileResult != null)
        {
            MediaPosterImage.Source = ImageSource.FromFile(fileResult.FullPath);

            // Save the file path for later use
            MediaPosterImage.BindingContext = fileResult.FullPath;
        }
    }

    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        // Validate input
        string title = MediaTitleEntry.Text;
        string genre = GenreEntry.Text;
        string tags = TagsEntry.Text;
        double rating = RatingStepper.Value;
        string posterPath = MediaPosterImage.BindingContext as string;

        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(genre) || string.IsNullOrWhiteSpace(tags) || posterPath == null)
        {
            await DisplayAlert("Error", "Please fill out all fields and upload a poster.", "OK");
            return;
        }

        // Check if the media already exists in the database
        if (Media.MediaDatabase.MediaExists(title))
        {
            await DisplayAlert("Error", "Media already exists in the database.", "OK");
            return;
        }

        // Save the data to the database
        Media.Media media = new Media.Media
        {
            Title = title,
            Genre = genre,
            Tags = tags.Split(',').Select(tag => tag.Trim()).ToList(),
            Rating = rating,
            PosterPath = posterPath
        };

        Media.MediaDatabase.SaveMedia(media);
        await DisplayAlert("Success", "Your rating has been submitted!", "OK");

        // Clear the form
        MediaTitleEntry.Text = "";
        GenreEntry.Text = "";
        TagsEntry.Text = "";
        MediaPosterImage.Source = null;
        RatingStepper.Value = 0;
    }
}