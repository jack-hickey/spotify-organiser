namespace site.Spotify
{
	public class SpotifyClient(string token)
	{
		public string Token { get; set; } = token;

		public async Task<IEnumerable<Track>> GetLikedSongsAsync()
		{
			using HttpClient client = new();
			HttpResponseMessage message = await client.GetAsync("https://api.spotify.com/v1/me/tracks");

			return [];
		}
	}
}