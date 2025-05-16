using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace site.Classes
{
	public class SpotifyPageModel : PageModel
	{
		public new User User { get; set; } = new();

		public async Task<IActionResult> OnGetAsync()
		{
			string token = await HttpContext.GetTokenAsync("access_token") ?? "";

			if (!string.IsNullOrWhiteSpace(token))
			{
				using HttpClient client = new();
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

				HttpResponseMessage response = await client.GetAsync("https://api.spotify.com/v1/me");

				if (response.IsSuccessStatusCode)
				{
					User = JsonSerializer.Deserialize<User>(await response.Content.ReadAsStringAsync()) ?? new();
				}
				else
				{
					Console.WriteLine($"Error: {await response.Content.ReadAsStringAsync()}");
				}
			}

			return Page();
		}
	}
}