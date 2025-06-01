using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using site.Spotify;

namespace site.Classes
{
	public class SpotifyPageModel : PageModel
	{
		private User? spotifyUser = null;

		public User SpotifyUser => spotifyUser ??= (JsonSerializer.Deserialize<User>(User.FindFirstValue("spotify_user") ?? "{}") ?? new());

		public string ClientID { get; set; } = Environment.GetEnvironmentVariable("SPOTIFY_CLIENT_ID") ?? "";

		public string Secret { get; set; } = Environment.GetEnvironmentVariable("SPOTIFY_CLIENT_SECRET") ?? "";

		private SpotifyClient? client = null;

		public async Task<SpotifyClient> GetClientAsync() => client ??= new(await HttpContext.GetTokenAsync("access_token") ?? "");
	}
}