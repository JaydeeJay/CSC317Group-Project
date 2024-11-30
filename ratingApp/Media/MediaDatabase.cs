namespace ratingApp.Media;

using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;

public static class MediaDatabase
{
    private static string FilePath => Path.Combine(Directory.GetCurrentDirectory(), "C:\\Users\\dick1\\source\\repos\\ratingApp\\ratingApp\\Media\\", "media.json");

    private static List<Media> mediaList;

    static MediaDatabase()
    {
        try
        {
            // Load media from file or initialize an empty list
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                mediaList = JsonSerializer.Deserialize<List<Media>>(json) ?? new List<Media>();
            }
            else
            {
                mediaList = new List<Media>();
                SaveToFile(); // Create an empty JSON file if it doesn't exist
            }
        }
        catch (JsonException)
        {
            // Handle invalid or empty JSON
            mediaList = new List<Media>();
            SaveToFile(); // Reset the file to a valid empty state
        }
    }

    public static void SaveMedia(Media media)
    {
        mediaList.Add(media);
        SaveToFile();
    }

    public static bool MediaExists(string title)
    {
        return mediaList.Any(m => m.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
    }

    private static void SaveToFile()
    {
        string json = JsonSerializer.Serialize(mediaList);
        File.WriteAllText(FilePath, json);
    }

    public static List<Media> GetAllMedia()
    {
        return mediaList;
    }
}
