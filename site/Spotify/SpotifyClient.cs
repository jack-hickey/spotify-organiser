using System.Text.Json;
using System.Text.Json.Nodes;

namespace site.Spotify
{
	public class SpotifyClient
	{
		public string Token { get; set; } = "";

		public HttpClient Client { get; set; }

		public async Task<IEnumerable<Track>> GetLikedSongsAsync()
		{
			HttpResponseMessage message = await Client.GetAsync("https://api.spotify.com/v1/me/tracks");

			if (await JsonSerializer.DeserializeAsync<JsonNode>(await message.Content.ReadAsStreamAsync()) is JsonNode response
			&& response["items"] is JsonNode items)
			{
				return items.AsArray().Select(x => x?["track"].Deserialize<Track>() ?? new());
			}

			return [];
		}

		public SpotifyClient(string token)
		{
			Token = token;

			Client = new();
			Client.DefaultRequestHeaders.Authorization = new("Bearer", token);
		}
	}
}