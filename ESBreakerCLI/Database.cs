using System;
using Contents;
using System.Collections.Generic;
using System.Reflection;
using NiceJson;

namespace ESBreakerCLI
{
	public class Database
	{
		bool prettyPrint = true;
		Dictionary<ContentsID, DatabaseFiler> files = new Dictionary<ContentsID, DatabaseFiler>();
		Dictionary<Type, Func<object, JsonArray, JsonArray>> parseStrategies = new Dictionary<Type, Func<object, JsonArray, JsonArray>>();



		List<ContentsID> supportedTypes = new List<ContentsID>
		{
			ContentsID.Text,
			ContentsID.Story
		};

		Dictionary<ContentsID, Type> contentTypes = new Dictionary<ContentsID, Type>()
		{
			{ ContentsID.Text, typeof(Contents.Text.Database) },
			{ ContentsID.Story, typeof(Contents.Story.Database) },
		};

		public Database(bool prettyPrint)
		{
			this.prettyPrint = prettyPrint;
			this.LoadStrategies();
		}

		public void InitFromFile()
		{
			foreach (ContentsID currentContent in supportedTypes)
			{
				var fm = new DatabaseFiler((int)currentContent);
				var format = fm.Load();
				var field = contentTypes[currentContent].GetField("tmpGeneralFormat",
									BindingFlags.Static |
									BindingFlags.NonPublic);
				field.SetValue(null, format);
				contentTypes[currentContent].GetMethod("Deploy").Invoke(null, null);
				files[currentContent] = fm;
			}
		}

		public void SaveToFile()
		{
			SaveText();
			SaveStory();
		}

		public void SaveText()
		{
			Console.WriteLine("Generating text patch");
			var fm = files[ContentsID.Text];
			var format = fm.Load();

			var dictFormat = new Dictionary<TextID, Contents.Text.Base.Format>();

			var field = contentTypes[ContentsID.Text].GetField("dictFormat",
								BindingFlags.Static |
								BindingFlags.NonPublic);
			dictFormat = (Dictionary<TextID, Contents.Text.Base.Format>)field.GetValue(null);

			foreach (KeyValuePair<TextID, Contents.Text.Base.Format> item in dictFormat)
			{
				var result = Serializer.SerializeMemory<Contents.Text.Base.Format>(item.Value);
				for (int i = 0; i < format.Information.Length; i++)
				{
					TextID assignID = (TextID)format.Information[i].AssignID;
					if (assignID == item.Key)
					{
						format.Information[i].Buffers = result;
						break;
					}
				}
			}

			fm.Save(format);
		}

		public void SaveStory()
		{
			Console.WriteLine("Generating story patch");
			var fm = files[ContentsID.Story];
			var format = fm.Load();

			var dictFormat = new Dictionary<StoryID, Contents.Story.Base.Format>();

			var field = contentTypes[ContentsID.Story].GetField("dictFormat",
								BindingFlags.Static |
								BindingFlags.NonPublic);
			dictFormat = (Dictionary<StoryID, Contents.Story.Base.Format>)field.GetValue(null);

			foreach (KeyValuePair<StoryID, Contents.Story.Base.Format> item in dictFormat)
			{
				var result = Serializer.SerializeMemory<Contents.Story.Base.Format>(item.Value);
				for (int i = 0; i < format.Information.Length; i++)
				{
					StoryID assignID = (StoryID)format.Information[i].AssignID;
					if (assignID == item.Key)
					{
						format.Information[i].Buffers = result;
						break;
					}
				}
			}

			fm.Save(format);
		}

		public void Parse()
		{
			var textTypes = Enum.GetValues(typeof(Contents.TextID));			
			Func<object, JsonArray, JsonArray> parse;
			foreach (Contents.TextID textType in textTypes)
			{
				var format = Contents.Text.Database.Get<Contents.Text.Base.Format>(textType);
				if (this.parseStrategies.TryGetValue(format.GetType(), out parse))
				{
					Console.WriteLine(String.Format("Exporting and updating {0} to json format", textType.ToString()));
					var output = parse(format, JsonFiler.GetExisting(textType.ToString()));
					JsonFiler.Store(textType.ToString(), output, prettyPrint);
				}
			}

			var storyTypes = Enum.GetValues(typeof(Contents.StoryID));
			foreach (Contents.StoryID storyType in storyTypes)
			{
				var format = Contents.Story.Database.Get<Contents.Story.Base.Format>(storyType);
				if (this.parseStrategies.TryGetValue(format.GetType(), out parse))
				{
					Console.WriteLine(String.Format("Exporting and updating {0} to json format", storyType.ToString()));
					var output = parse(format, JsonFiler.GetExisting(storyType.ToString()));
					JsonFiler.Store(storyType.ToString(), output,prettyPrint);
				}
			}
		}

		void LoadStrategies()
		{
			parseStrategies.Add(typeof(Contents.Text.UI.Format), ParseTextStrategies.UIText);
			parseStrategies.Add(typeof(Contents.Text.Illustrator.Format), ParseTextStrategies.IllustratorText);
			parseStrategies.Add(typeof(Contents.Text.ChipExplain.Format), ParseTextStrategies.ChipExplain);
			parseStrategies.Add(typeof(Contents.Text.Name.Format), ParseTextStrategies.Name);
			// Adding another single Text.Name file was too hard? That you decided to copypasta it into text charged?
			parseStrategies.Add(typeof(Contents.Text.Charged.Format), ParseTextStrategies.Charged);
			// Yay! more copypasta...why are we even exporting this? Beats me. At least we laugh a bit with poor Teemo 
			// who's censored on PSO.
			parseStrategies.Add(typeof(Contents.Text.NGWord.Format), ParseTextStrategies.NGWord);
			parseStrategies.Add(typeof(Contents.Text.SeraphyRoom.Format), ParseTextStrategies.SeraphyRoom);
			parseStrategies.Add(typeof(Contents.Text.Item.Format), ParseTextStrategies.Item);
			parseStrategies.Add(typeof(Contents.Text.Explain.Format), ParseTextStrategies.Explain);
			parseStrategies.Add(typeof(Contents.Story.Title.Format), ParseStoryStrategies.Title);
			parseStrategies.Add(typeof(Contents.Story.Text.Format), ParseStoryStrategies.Text);
		}
	}
}
