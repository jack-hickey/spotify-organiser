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
			string[] urls = GenreClient.GetURLs(await (await GetClientAsync()).GetLikedSongsAsync());

			return new OkResult();
		}
	}
}