namespace site.Spotify
{
	public class GenreClient
	{
		private const int URL_LIMIT = 6957;

		public static string[] GetURLs(IEnumerable<Track> tracks)
		{
			List<string> urls = [];
			int total = tracks.Count();
			string mbURL = "";

			for (int ix = 0; ix < total; ++ix)
			{
				Track track = tracks.ElementAt(ix);
				string current = $"%20OR%20({track.MbRequest})";

				if ((mbURL + current).Length <= URL_LIMIT)
				{
					mbURL += current;
				}
				else
				{
					urls.Add(mbURL[8..]);

					if (ix == total - 1)
					{
						urls.Add($"({track.MbRequest})");
					}
					else
					{
						mbURL = current;
					}
				}
			}

			if (!string.IsNullOrWhiteSpace(mbURL)) { urls.Add(mbURL[8..]); }

			return [.. urls];
		}
	}
}