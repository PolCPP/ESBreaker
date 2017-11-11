using System;
using NiceJson;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using System.Globalization;
using System.Security;

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
				string FileData = null;
				try
				{
					FileData = File.ReadAllText(filePath);
				}
				catch (ArgumentNullException ex)
				{
					Console.Error.WriteLine("No Filename");
					Console.Error.WriteLine(ex.ToString());
				}
				catch (ArgumentException ex)
				{
					Console.Error.WriteLine("Bad Filename: " + filePath);
					Console.Error.WriteLine(ex.ToString());
				}
				catch (PathTooLongException ex)
				{
					Console.Error.WriteLine("Filename too long: "+ filePath);
					Console.Error.WriteLine(ex.ToString());
				}
				catch (DirectoryNotFoundException ex)
				{
					Console.Error.WriteLine("Could not find directory: "+ filePath);
					Console.Error.WriteLine(ex.ToString());
				}
				catch (FileNotFoundException ex)
				{
					Console.Error.WriteLine("File does not exists: " + filePath);
					Console.Error.WriteLine(ex.ToString());
				}
				catch (IOException ex)
				{
					Console.Error.WriteLine("I/O Error while reading: "+ filePath);
					Console.Error.WriteLine(ex.ToString());
				}
				catch (UnauthorizedAccessException ex)
				{
					Console.Error.WriteLine("The reading operation is not supported: "+ filePath);
					Console.Error.WriteLine(ex.ToString());
				}
				catch (NotSupportedException ex)
				{
					Console.Error.WriteLine("Filepath is invalid: "+ filePath);
					Console.Error.WriteLine(ex.ToString());
				}
				catch (SecurityException ex)
				{
					Console.Error.WriteLine("Blocked from reading: "+ filePath);
					Console.Error.WriteLine(ex.ToString());
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine("Unknown issue with: " + filePath);
					Console.Error.WriteLine(ex.ToString());
					throw;
				}

				try
				{
					existingData = (JsonArrayCollection)JsonNode.ParseJsonString(FileData);
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine("Cannot parse json of " + filePath);
					Console.Error.WriteLine(ex.ToString());
					throw;
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
