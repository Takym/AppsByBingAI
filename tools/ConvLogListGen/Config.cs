using System.Text.Json.Serialization;

namespace ConvLogListGen
{
	public sealed record Config
	{
		[JsonPropertyName("input")]
		public string? InputDirectory { get; init; }

		[JsonPropertyName("pattern")]
		public string? InputPattern { get; init; }

		[JsonPropertyName("output")]
		public string? OutputFile { get; init; }

		[JsonPropertyName("title")]
		public string? OutputTitle { get; init; }

		[JsonPropertyName("datefmt")]
		public string? OutputDateFormat { get; init; }
	}
}
