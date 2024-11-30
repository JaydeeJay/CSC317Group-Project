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
}