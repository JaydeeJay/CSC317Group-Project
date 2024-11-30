namespace ratingApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();


            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(ExplorePage), typeof(ExplorePage));
            Routing.RegisterRoute(nameof(RatingPage), typeof(RatingPage));
            Routing.RegisterRoute(nameof(BookmarkPage), typeof(BookmarkPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));


        }
    }
}
