using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using site.Classes;

namespace site.Pages
{
	public class IndexModel : SpotifyPageModel
	{
		public IActionResult OnGetLogin()
		{
			return Challenge(new AuthenticationProperties(), "Spotify");
		}

		public IActionResult OnPostOrganise()
		{
			return new OkResult();
		}
	}
}