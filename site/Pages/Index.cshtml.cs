using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using site.Classes;
using site.Spotify;

namespace site.Pages
{
	public class IndexModel : SpotifyPageModel
	{
		public IActionResult OnGetLogin()
		{
			return Challenge(new AuthenticationProperties(), "Spotify");
		}

		public async Task<IActionResult> OnPostOrganise()
		{
			Console.WriteLine((await (await GetClientAsync()).GetLikedSongsAsync()).Count());
			return new OkResult();
		}
	}
}