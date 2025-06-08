using YoutubeExplode;

public class YouTubeTranscriptRetriever
{
    private readonly YoutubeClient _youtubeClient;

    public YouTubeTranscriptRetriever(YoutubeClient youtubeClient)
    {
        _youtubeClient = youtubeClient ?? throw new ArgumentNullException(nameof(youtubeClient), "YouTube client cannot be null");
    }

    public async Task<(string Transcript, string Error)> GetTranscriptAsync(string videoId, string languageCode = "en")
    {
        (string Transcript, string Error) result;

        try
        {
            var trackManifest = await _youtubeClient.Videos.ClosedCaptions.GetManifestAsync(videoId);
            var trackInfo = trackManifest.GetByLanguage(languageCode) ?? trackManifest.TryGetByLanguage("en");

            if (trackInfo == null)
            {
                result = ("", "No transcript available for the specified language");
                return result;
            }

            var track = await _youtubeClient.Videos.ClosedCaptions.GetAsync(trackInfo);
            result = (string.Join("", track.Captions.Select(c => c.Text)), "");
        }
        catch (Exception ex)
        {
            result = ("", $"Error retrieving transcript: {ex.Message}");
        }

        return result;
    }
}