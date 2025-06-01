using System.Collections.Specialized;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Web;
using site.Classes;

namespace site.Spotify
{
	public class SpotifyClient
	{
		public string Token { get; set; } = "";

		public HttpClient Client { get; set; }

		public async Task<IEnumerable<Track>> GetLikedSongsAsync()
		{
			if (await GetArrayResponse("https://api.spotify.com/v1/me/tracks") is JsonNode response)
			{
				return response.AsArray().Select(x => x?["track"].Deserialize<Track>() ?? new());
			}

			return [];
		}

		private async Task<JsonNode> GetArrayResponse(string url)
		{
			JsonArray array = [];

			string next = url;
			string current = "";

			async Task<JsonNode?> getResponse(string currentURL)
			{
				Uri uri = new(currentURL);

				HttpResponseMessage message = await Client.GetAsync(uri.ChangeQueryParameter("limit", 50));

				if (await JsonSerializer.DeserializeAsync<JsonNode>(await message.Content.ReadAsStreamAsync()) is JsonNode response
				&& response["items"] is JsonNode items)
				{
					current = next;
					next = response["next"]?.ToString() ?? "";

					return items;
				}
				else
				{
					next = "";
					return null;
				}
			}

			while (!string.IsNullOrWhiteSpace(next) && !next.Equals(current))
			{
				if (await getResponse(next) is JsonNode items)
				{
					foreach (JsonNode? item in items.AsArray())
					{
						array.Add(item?.DeepClone());
					}
				}
			}

			return array;
		}

		public SpotifyClient(string token)
		{
			Token = token;

			Client = new();
			Client.DefaultRequestHeaders.Authorization = new("Bearer", token);
		}
	}
}