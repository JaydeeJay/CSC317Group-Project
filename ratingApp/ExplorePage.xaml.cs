using System.Text.Json;

namespace ratingApp;



public partial class ExplorePage : ContentPage
{
    public List<Media.Media> MediaList { get; set; }

    public ExplorePage()
    {
        InitializeComponent();

        // Ensure MediaList is initialized even if no data is present
        MediaList = Media.MediaDatabase.GetAllMedia() ?? new List<Media.Media>();

        BindingContext = this;

        Console.WriteLine($"Media Count: {MediaList.Count}");
        foreach (var media in MediaList)
        {
            Console.WriteLine($"Title: {media.Title}, Genre: {media.Genre}");
        }
    }


    private List<Media.Media> BookmarkedMedia = new List<Media.Media>();

    private void BookmarkMedia_Clicked(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            // Find the media associated with the button
            var media = (button.BindingContext as Media.Media);

            if (media != null && !BookmarkedMedia.Contains(media))
            {
                BookmarkedMedia.Add(media);
                SaveBookmarks();
                DisplayAlert("Bookmark Added", $"{media.Title} has been added to your bookmarks.", "OK");
            }
        }
    }

    // Save Bookmarks to File (can be extended for persistence)
    private void SaveBookmarks()
    {
        string filePath = Path.Combine(FileSystem.AppDataDirectory, "bookmarks.json");
        string json = JsonSerializer.Serialize(BookmarkedMedia);
        File.WriteAllText(filePath, json);
    }


}
