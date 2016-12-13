using System;
using NiceJson;
using System.IO;
using System.Reflection;

namespace ESBreakerCLI
{
	public static class JsonFiler
	{
		private static string path = Path.Combine(System.IO.Path.GetDirectoryName(
										Assembly.GetEntryAssembly().Location), "json");

		private static string extension = ".txt";

		public static JsonArray GetExisting(string fileName)
		{
			var existingData = new JsonArray();
			var filePath = Path.Combine(path, fileName + extension);
			if (File.Exists(filePath))
			{
				try
				{
					existingData = (JsonArray)JsonNode.ParseJsonString(File.ReadAllText(filePath));
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine("Cannot parse json on " + filePath);
					Console.Error.WriteLine(ex.ToString());
				}

			}
			return existingData;
		}

		public static void Store(string fileName, JsonArray contents, bool prettyPrint)
		{
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			var filePath = Path.Combine(JsonFiler.path, fileName + extension);
			if (contents.Count != 0)
			{
				var data = prettyPrint ? contents.ToJsonPrettyPrintString() : contents.ToJsonString();
				if (!data.EndsWith(Environment.NewLine, StringComparison.Ordinal))
				{
					data += '\n';
				}
				File.WriteAllText(filePath, data);
			}			        
		}
	}
}
