using System.Text.Json.Serialization;

namespace site.Spotify
{
	public class Track
	{
		[JsonPropertyName("name")]
		public string Name { get; set; } = "";

		private string? mbRequest;

		public string MbRequest => mbRequest ??= $"title:{Name} AND artist:{Artists.FirstOrDefault()?.Name ?? ""}";

		[JsonPropertyName("artists")]
		public Artist[] Artists { get; set; } = [];
	}
}