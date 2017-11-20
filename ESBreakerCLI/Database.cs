using System;
using Contents;
using System.Collections.Generic;
using System.Reflection;
using NiceJson;
using System.Globalization;

namespace ESBreakerCLI
{
	public class Database
	{
		bool saveJson;
		bool prettyPrint;
		bool saveDatabase;

		Dictionary<ContentsID, DatabaseFiler> files = new Dictionary<ContentsID, DatabaseFiler>();
		Dictionary<Type, Func<object, JsonArrayCollection, bool, JsonArrayCollection>> parseStrategies = new Dictionary<Type, Func<object, JsonArrayCollection, bool, JsonArrayCollection>>();

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

		public Database(bool saveJson, bool prettyPrint, bool saveDatabase)
		{
			this.saveJson = saveJson;
			this.prettyPrint = prettyPrint;
			this.saveDatabase = saveDatabase;
			this.LoadStrategies();
		}

		public void Process()
		{
			InitFromFile();
			Parse();
			if (saveDatabase)
				SaveToFile();
		}

		void InitFromFile()
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

		void SaveToFile()
		{
			SaveText();
			SaveStory();
		}

		void SaveText()
		{
			Console.WriteLine("Generating text patch");
			var fm = files[ContentsID.Text];
			var format = fm.Load();

			var dictFormat = new Dictionary<TextID, Contents.Text.Base.Format>();

			var field = contentTypes[ContentsID.Text].GetField("dictFormat",
								BindingFlags.Static |
								BindingFlags.NonPublic);
			dictFormat = (Dictionary<TextID, Contents.Text.Base.Format>)field.GetValue(null);

			for (int i = 0; i < format.Information.Length; i++)
			{
				var assignID = (TextID)format.Information[i].AssignID;
				if (dictFormat.ContainsKey(assignID))
				{
					format.Information[i].Buffers = Serializer.SerializeMemory<Contents.Text.Base.Format>(dictFormat[assignID]);
				}
			}

			fm.Save(format);
		}

		void SaveStory()
		{
			Console.WriteLine("Generating story patch");
			var fm = files[ContentsID.Story];
			var format = fm.Load();

			var dictFormat = new Dictionary<StoryID, Contents.Story.Base.Format>();

			var field = contentTypes[ContentsID.Story].GetField("dictFormat",
								BindingFlags.Static |
								BindingFlags.NonPublic);
			dictFormat = (Dictionary<StoryID, Contents.Story.Base.Format>)field.GetValue(null);

			for (int i = 0; i < format.Information.Length; i++)
			{
				var assignID = (StoryID)format.Information[i].AssignID;
				if (dictFormat.ContainsKey(assignID))
				{
					format.Information[i].Buffers = Serializer.SerializeMemory<Contents.Story.Base.Format>(dictFormat[assignID]);
				}
			}

			fm.Save(format);
		}

		void Parse()
		{
			var textTypes = Enum.GetValues(typeof(Contents.TextID));
			Func<object, JsonArrayCollection, bool, JsonArrayCollection> parse;
			foreach (Contents.TextID textType in textTypes)
			{
				var format = Contents.Text.Database.Get<Contents.Text.Base.Format>(textType);
				if (this.parseStrategies.TryGetValue(format.GetType(), out parse))
				{
					Console.WriteLine(String.Format(CultureInfo.InvariantCulture, "Processing {0}", textType.ToString()));
					var output = parse(format, JsonFiler.GetExisting(textType.ToString()), saveJson);
					if (saveJson)
						JsonFiler.Store(textType.ToString(), output, prettyPrint);
				}
			}

			var storyTypes = Enum.GetValues(typeof(Contents.StoryID));
			foreach (Contents.StoryID storyType in storyTypes)
			{
				var format = Contents.Story.Database.Get<Contents.Story.Base.Format>(storyType);
				if (this.parseStrategies.TryGetValue(format.GetType(), out parse))
				{
					Console.WriteLine(String.Format(CultureInfo.InvariantCulture, "Processing {0}", storyType.ToString()));
					var output = parse(format, JsonFiler.GetExisting(storyType.ToString()), saveJson);
					JsonFiler.Store(storyType.ToString(), output,prettyPrint);
				}
			}
		}

		void LoadStrategies()
		{
			parseStrategies.Add(typeof(Contents.Text.UI.Format), ParseTextStrategies.UIText);
			parseStrategies.Add(typeof(Contents.Text.Illustrator.Format), ParseTextStrategies.IllustratorText);
			parseStrategies.Add(typeof(Contents.Text.ChipExplain.Format), ParseTextStrategies.ChipExplain);
			parseStrategies.Add(typeof(Contents.Text.Leisure_PhotonDice_SpeakText.Format), ParseTextStrategies.PhotonDice);
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
