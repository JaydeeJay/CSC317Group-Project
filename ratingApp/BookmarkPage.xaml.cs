using System.Text.Json;

namespace ratingApp;

public partial class BookmarkPage : ContentPage
{
    public List<Media.Media> BookmarkedMedia { get; set; }

    public BookmarkPage()
    {
        InitializeComponent();

        // Load bookmarked media from storage
        BookmarkedMedia = LoadBookmarks() ?? new List<Media.Media>();

        // Set BindingContext for data binding
        BindingContext = this;

        Console.WriteLine($"Loaded {BookmarkedMedia.Count} bookmarked items.");
    }

    private List<Media.Media> LoadBookmarks()
    {
        string filePath = Path.Combine(FileSystem.AppDataDirectory, "bookmarks.json");

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Media.Media>>(json);
        }

        return null;
    }
}