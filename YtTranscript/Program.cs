try
{
    var appRunner = new YtTranscript.AppRunner();
    await appRunner.Run(args);
}
catch (System.Exception)
{
    Console.WriteLine("An error occurred while running the application. Please ensure you have provided a valid YouTube video ID and that the YouTube API is accessible.");
    Environment.Exit(1);
}