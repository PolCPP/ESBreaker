using System;
using System.Collections.Generic;
using NiceJson;

namespace ESBreakerCLI
{
	public static class ParseStoryStrategies
	{
		public static JsonArrayCollection Text(object source, JsonArrayCollection existingData, bool saveJson)
		{
			JsonArrayCollection data = new JsonArrayCollection();

			Contents.Story.Text.Format storyEventFormat = (Contents.Story.Text.Format)source;
			if (storyEventFormat != null)
			{
				foreach (Contents.Story.Text.Information information in storyEventFormat.Information)
				{
					int i = 0;
					foreach (var param in information.Parameter)
					{
						JsonObjectCollection savedItem = default(JsonObjectCollection);

						if (existingData != null)
						{
							int Count = existingData.Count;
							for (int idx = 0; idx < Count; idx++)
							{
								if (existingData[idx]["jp_text"] == param.Text
								    && existingData[idx]["jp_name"] == param.Name)
								{
									savedItem = (JsonObjectCollection)existingData[idx];
									existingData.RemoveAt(idx);
									break;
								}
							}
						}

                        var selectButtons = new JsonArrayCollection();
                        var translatedButtons = new JsonArrayCollection();
                        foreach (var button in param.SelectButton)
                        {
                            selectButtons.Add(button.Name);
                            translatedButtons.Add("");
                        }
                        JsonObjectCollection item = new JsonObjectCollection();
						item["eventNo"] = param.EventNo;
						item["jp_name"] = param.Name;
						item["tr_name"] = "";
						item["jp_text"] = param.Text;
                        item["tr_text"] = "";
                        item["jp_buttons"] = selectButtons;
                        item["tr_buttons"] = translatedButtons;
                        item["fileID"] = information.FileID;
						if (savedItem != default(JsonObjectCollection))
						{
							if (!String.IsNullOrEmpty(savedItem["tr_name"]))
							{
								param.Name = savedItem["tr_name"];
								item["tr_name"] = savedItem["tr_name"];
							}
							if (!String.IsNullOrEmpty(savedItem["tr_text"]))
							{
								param.Text = savedItem["tr_text"];
								item["tr_text"] = savedItem["tr_text"];
							}
                            if (savedItem.ContainsKey("tr_buttons"))
                            {
                                var idx = 0;
                                foreach (var button in (JsonArrayCollection)savedItem["tr_buttons"] )
                                {
                                    if (!String.IsNullOrEmpty(button) && param.SelectButton.Length > idx)
                                    {
                                            param.SelectButton[idx].Name = button;
                                    }
                                    idx++;
                                }
                                item["tr_buttons"] = savedItem["tr_buttons"];
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

		public static JsonArrayCollection Title(object source, JsonArrayCollection existingData, bool saveJson)
		{
			JsonArrayCollection data = new JsonArrayCollection();

			Contents.Story.Title.Format storyTitle = (Contents.Story.Title.Format)source;
			if (storyTitle != null)
			{

				foreach (Contents.Story.Title.Information information in storyTitle.Information)
				{
					int i = 0;
					for (int idx = 0; idx < information.TitleList.Length; idx++)
					{
						var param = information.TitleList[idx];
						JsonObjectCollection savedItem = default(JsonObjectCollection);

						if (existingData != null)
						{
							int Count = existingData.Count;
							for (int idy = 0; idy < Count; idy++)
							{
								if (existingData[idy] != null &&
								    existingData[idy]["jp_title"] == param)
								{
									savedItem = (JsonObjectCollection)existingData[idy];
									existingData.RemoveAt(idy);
									break;
								}
							}
						}

						JsonObjectCollection item = new JsonObjectCollection();
						item["title_id"] = information.TitleID;
						item["jp_title"] = param;
						item["tr_title"] = "";
						if (savedItem != default(JsonObjectCollection))
						{
							if (!String.IsNullOrEmpty(savedItem["tr_title"]))
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
