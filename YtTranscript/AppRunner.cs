using System.Text.RegularExpressions;

namespace YtTranscript
{
    public class AppRunner
    {
        public async Task Run(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string videoId = string.Empty;
            string languageCode = "en";
            foreach (var arg in args)
            {
                if (arg.StartsWith("--videoId="))
                    videoId = arg.Substring("--videoId=".Length);
                else if (arg.StartsWith("--lang="))
                    languageCode = arg.Substring("--lang=".Length);
            }

            if (string.IsNullOrEmpty(videoId))
            {
                Console.WriteLine("Please provide a YouTube video ID using --videoId=<id>.");
                return;
            }

            var youTubeTranscriptRetriever = new YouTubeTranscriptRetriever(new YoutubeExplode.YoutubeClient());
            var result = await youTubeTranscriptRetriever.GetTranscriptAsync(videoId, languageCode);

            if (!string.IsNullOrEmpty(result.Error))
            {
                Console.WriteLine($"Error: {result.Error}");
                return;
            }

            var cleanedTranscript = Regex.Replace(result.Transcript, @"\r\n|\n|\r|\s{2,}", " ");
            Console.WriteLine(cleanedTranscript);
        }
    }
}