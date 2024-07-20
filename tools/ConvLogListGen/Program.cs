using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ConvLogListGen
{
	internal static class Program
	{
		private static void Run(string configPath)
		{
			Config? config;
			using (var fs = new FileStream(configPath, FileMode.Open, FileAccess.Read, FileShare.Read)) {
				config = JsonSerializer.Deserialize<Config>(fs);
			}

			if (config is null) {
				Console.WriteLine("設定ファイルを正しく読み込めませんでした。");
				return;
			}

			if (config.InputDirectory is null) {
				Console.WriteLine("入力ディレクトリが設定されていません。");
				return;
			}

			if (config.InputPattern is null) {
				Console.WriteLine("入力ファイルの正規表現が設定されていません。");
				return;
			}

			if (config.OutputFile is null) {
				Console.WriteLine("出力ファイルが設定されていません。");
				return;
			}

			if (config.OutputTitle is null) {
				Console.WriteLine("出力ファイルの題名が設定されていません。");
				return;
			}

			if (config.OutputDateFormat is null) {
				Console.WriteLine("出力ファイルの日付書式が設定されていません。");
				return;
			}

			var sb = new StringBuilder();
			sb.AppendFormat("# {0}", config.OutputTitle).AppendLine().AppendLine();

			string[] files = Directory.GetFiles(config.InputDirectory, "*", SearchOption.AllDirectories);
			for (int i = 0; i < files.Length; ++i) {
				string fname   = files[i].Replace('\\', '/');
				var    matches = Regex.Matches(fname, config.InputPattern);

				if (matches.Count == 1) {
					string year = "0000", month = "00", day = "00";

					var groups     = matches[0].Groups;
					int groupCount = groups.Count;
					for (int j = 0; j < groupCount; ++j) {
						var group = groups[j];

						switch (group.Name) {
						case nameof(year):
							year = group.Value;
							break;
						case nameof(month):
							month = group.Value;
							break;
						case nameof(day):
							day = group.Value;
							break;
						}
					}

					string title = "<Unknown>";
					using (var sr = new StreamReader(fname, Encoding.UTF8)) {
						if (sr.ReadLine() is not null and var line &&
							line.StartsWith("# ")) {
							title = line[2..];
						}
					}

					sb
						.AppendFormat("* [{0}]({1}) ", title, fname)
						.AppendFormat(config.OutputDateFormat, year, month, day)
						.AppendLine();

					PrintStatus("追加");
				} else {
					PrintStatus("無視");
				}

				void PrintStatus(string result)
				{
					int index = i + 1;

					Console.WriteLine(
						"{0:000.00%}({1,10}/{2,10}):{3}:{4}...",
						((double)(index)) / files.Length,
						index,
						files.Length,
						result,
						fname
					);
				}
			}

			using (var sw = new StreamWriter(config.OutputFile, false, Encoding.UTF8)) {
				sw.Write(sb.ToString());
			}
		}

		[STAThread()]
		private static int Main(string[] args)
		{
			try {
				Run(args.Length == 1 ? args[0] : "convlog_list_config.json");
				return 0;
			} catch (Exception e) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Error.WriteLine();
				Console.Error.WriteLine(e.ToString());
				Console.ResetColor();
				return e.HResult;
			}
		}
	}
}
