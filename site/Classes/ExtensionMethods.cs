using Microsoft.AspNetCore.Mvc;

namespace site.Classes
{
	public static class ExtensionMethods
	{
		public static string GetTopLevel(this IUrlHelper helper, HttpContext context)
		{
			Uri uri = new(context.Request.Scheme + "://" + context.Request.Host + helper.Content("~/"));

			string url = uri.ToString().ToLower();
			return url.EndsWith('/') ? url : url + '/';
		}
	}
}