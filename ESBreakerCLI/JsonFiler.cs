using System;
using NiceJson;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using System.Globalization;

namespace ESBreakerCLI
{
	public static class JsonFiler
	{
		private static string path = Path.Combine(System.IO.Path.GetDirectoryName(
		                                          Assembly.GetEntryAssembly().Location), "json");

		private static string extension = ".txt";

		public static JsonArrayCollection GetExisting(string fileName)
		{
			var existingData = new JsonArrayCollection();
			var filePath = Path.Combine(path, fileName + extension);
			if (File.Exists(filePath))
			{
				try
				{
					existingData = (JsonArrayCollection)JsonNode.ParseJsonString(File.ReadAllText(filePath));
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine("Cannot parse json on " + filePath);
					Console.Error.WriteLine(ex.ToString());
				}

			}
			return existingData;
		}

		public static string JsonPrettify(string json)
		{
			using (var stringReader = new StringReader(json))
			using (var stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				var jsonReader = new JsonTextReader(stringReader);
				var jsonWriter = new JsonTextWriter(stringWriter) { Formatting = Formatting.Indented, Indentation = 1, IndentChar = '\t' };
				jsonWriter.WriteToken(jsonReader);
				return stringWriter.ToString();
			}
		}

		public static void Store(string fileName, JsonArrayCollection contents, bool prettyPrint)
		{
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			var filePath = Path.Combine(JsonFiler.path, fileName + extension);
			if (contents != null && contents.Count != 0)
			{
				var data = prettyPrint ? JsonPrettify(contents.ToJsonString()) : contents.ToJsonString();
				if (!data.EndsWith(Environment.NewLine, StringComparison.Ordinal))
				{
					data += '\n';
				}
				File.WriteAllText(filePath, data);
			}
		}
	}
}
