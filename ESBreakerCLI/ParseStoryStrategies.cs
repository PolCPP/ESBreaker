using NiceJson;

namespace ESBreakerCLI
{
	public static class ParseStoryStrategies
	{
		public static JsonArray Text(object source, JsonArray existingData, bool saveJson)
		{
			JsonArray data = new JsonArray();

			Contents.Story.Text.Format storyEventFormat = (Contents.Story.Text.Format)source;
			if (storyEventFormat != null)
			{
				foreach (Contents.Story.Text.Information information in storyEventFormat.Information)
				{
					int i = 0;
					foreach (var param in information.Parameter)
					{
						JsonObject savedItem = default(JsonObject);
						for (int idx = 0; idx < existingData.Count; idx++)
						{
							if (existingData[idx]["jp_text"] == param.Text
								&& existingData[idx]["jp_name"] == param.Name)
							{
								savedItem = (JsonObject)existingData[idx];
								existingData.RemoveAt(idx);
								break;
							}
						}

						JsonObject item = new JsonObject();
						item["eventNo"] = param.EventNo;
						item["jp_name"] = param.Name;
						item["tr_name"] = "";
						item["jp_text"] = param.Text;
						item["tr_text"] = "";
						item["fileID"] = information.FileID;
						if (savedItem != default(JsonObject))
						{
							if (savedItem["tr_name"] != "")
							{
								param.Name = savedItem["tr_name"];
								item["tr_name"] = savedItem["tr_name"];
							}
							if (savedItem["tr_text"] != "")
							{
								param.Text = savedItem["tr_text"];
								item["tr_text"] = savedItem["tr_text"];
							}
						}
						i++;
						if (saveJson)
						{
							data.Add(item);
						}
					}

				}
			}
			return data;
		}

		public static JsonArray Title(object source, JsonArray existingData, bool saveJson)
		{
			JsonArray data = new JsonArray();

			Contents.Story.Title.Format storyTitle = (Contents.Story.Title.Format)source;
			if (storyTitle != null)
			{

				foreach (Contents.Story.Title.Information information in storyTitle.Information)
				{
					int i = 0;
					for (int idx = 0; idx < information.TitleList.Length; idx++)
					{
						var param = information.TitleList[idx];
						JsonObject savedItem = default(JsonObject);
						for (int idy = 0; idy < existingData.Count; idy++)
						{
							if (existingData[idy]["jp_title"] == param)
							{
								savedItem = (JsonObject)existingData[idy];
								existingData.RemoveAt(idy);
								break;
							}
						}

						JsonObject item = new JsonObject();
						item["title_id"] = information.TitleID;
						item["jp_title"] = param;
						item["tr_title"] = "";
						if (savedItem != default(JsonObject))
						{
							if (savedItem["tr_title"] != "")
							{
								information.TitleList[idx] = savedItem["tr_title"];
								item["tr_title"] = savedItem["tr_title"];
							}
						}
						i++;
						if (saveJson)
						{
							data.Add(item);
						}
					}

				}
			}
			return data;
		}
	}
}
