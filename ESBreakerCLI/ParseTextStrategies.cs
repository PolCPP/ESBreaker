using System;
using NiceJson;
using System.Globalization;

namespace ESBreakerCLI
{
	public static class ParseTextStrategies
	{
		public static JsonArrayCollection UIText(object source, JsonArrayCollection existingData, bool saveJson)
		{
			JsonArrayCollection data = new JsonArrayCollection();
			Contents.Text.UI.Format textUIFormat = (Contents.Text.UI.Format)source;
			string currentAssign;

			foreach (Contents.Text.UI.Information information in textUIFormat.Information)
			{
				JsonObjectCollection savedItem = default(JsonObjectCollection);
				JsonObjectCollection item = new JsonObjectCollection();
				if (information.Assign.GetType() == typeof(Contents.Text.Assign.TypeInt))
				{
					currentAssign = ((Contents.Text.Assign.TypeInt)information.Assign).Param.ToString(CultureInfo.InvariantCulture);
				}
				else
				{
					currentAssign = ((Contents.Text.Assign.TypeString)information.Assign).Param.Trim();
				}

				if (existingData != null)
				{
					int Count = existingData.Count;
					for (int idx = 0; idx < Count; idx++)
					{
						if (existingData[idx] != null &&
						    existingData[idx]["jp_text"] == information.Text)
						{
							savedItem = (JsonObjectCollection)existingData[idx];
							existingData.RemoveAt(idx);
							break;
						}
					}
				}

				item["assign"] = currentAssign;
				item["jp_text"] = information.Text;
				item["tr_text"] = "";

				if (savedItem != default(JsonObjectCollection))
				{
					if (!String.IsNullOrEmpty(savedItem["tr_text"]))
					{
						information.Text = savedItem["tr_text"];
						item["tr_text"] = savedItem["tr_text"];
					}
				}
				if (saveJson)
				{
					data.Add(item);
				}
			}
			return data;
		}


		public static JsonArrayCollection PhotonDice(object source, JsonArrayCollection existingData, bool saveJson)
		{
			JsonArrayCollection data = new JsonArrayCollection();
			Contents.Text.Leisure_PhotonDice_SpeakText.Format diceFormat = (Contents.Text.Leisure_PhotonDice_SpeakText.Format)source;
			string currentAssign;

			foreach (Contents.Text.Leisure_PhotonDice_SpeakText.Information information in diceFormat.Information)
			{
				JsonObjectCollection savedItem = default(JsonObjectCollection);
				JsonObjectCollection item = new JsonObjectCollection();
				if (information.Assign.GetType() == typeof(Contents.Text.Assign.TypeInt))
				{
					currentAssign = ((Contents.Text.Assign.TypeInt)information.Assign).Param.ToString(CultureInfo.InvariantCulture);
				}
				else if (information.Assign.GetType() == typeof(Contents.Text.Assign.Type2Int))
				{
					Contents.Text.Assign.Type2Int assign = (Contents.Text.Assign.Type2Int)information.Assign;
					currentAssign = "";
					for (int i = 0; i < assign.Param.Length; i++)
						currentAssign += assign.Param[i].ToString(CultureInfo.InvariantCulture);
				}
				else
				{
					currentAssign = ((Contents.Text.Assign.TypeString)information.Assign).Param.Trim();
				}


				if (existingData != null)
				{
					int Count = existingData.Count;
					for (int idx = 0; idx < Count; idx++)
					{
						if (existingData[idx] != null &&
						    existingData[idx]["assign"] == currentAssign)
						{
							savedItem = (JsonObjectCollection)existingData[idx];
							existingData.RemoveAt(idx);
							break;
						}
					}
				}

				var patterns = new JsonArrayCollection();
				var translatedPatterns = new JsonArrayCollection();

				foreach (var pattern in information.Pattern)
				{
					patterns.Add(pattern);
					translatedPatterns.Add("");
				}

				item["assign"] = currentAssign;
				item["jp_patterns"] = patterns;
				item["tr_patterns"] = translatedPatterns;


				if (savedItem != default(JsonObjectCollection))
				{
					if (savedItem.ContainsKey("tr_patterns"))
					{
						var idx = 0;
						foreach (var pattern in (JsonArrayCollection)savedItem["tr_patterns"])
						{
							if (!String.IsNullOrEmpty(pattern) && information.Pattern.Length > idx)
							{
								information.Pattern[idx] = pattern;
							}
							idx++;
						}
						item["tr_patterns"] = savedItem["tr_patterns"];
					}
				}
				if (saveJson)
				{
					data.Add(item);
				}
			}
			return data;
		}


		public static JsonArrayCollection IllustratorText(object source, JsonArrayCollection existingData, bool saveJson)
		{
			JsonArrayCollection data = new JsonArrayCollection();
			Contents.Text.Illustrator.Format textUIFormat = (Contents.Text.Illustrator.Format)source;
			string chipId;

			foreach (Contents.Text.Illustrator.Information information in textUIFormat.Information)
			{
				JsonObjectCollection savedItem = default(JsonObjectCollection);
				JsonObjectCollection item = new JsonObjectCollection();

				chipId = ((Contents.Text.Assign.TypeInt)information.Assign).Param.ToString(CultureInfo.InvariantCulture);

				if (existingData != null)
				{
					int Count = existingData.Count;
					for (int idx = 0; idx < Count; idx++)
					{
						if (existingData[idx] != null &&
						    existingData[idx]["jp_name"] == information.Name)
						{
							savedItem = (JsonObjectCollection)existingData[idx];
							existingData.RemoveAt(idx);
							break;
						}
					}
				}

				item["chip"] = chipId;
				item["jp_CV"] = information.CV;
				item["tr_CV"] = "";
				item["jp_name"] = information.Name;
				item["tr_name"] = "";

				if (savedItem != default(JsonObjectCollection))
				{
					if (!String.IsNullOrEmpty(savedItem["tr_name"]))
					{
						information.Name = savedItem["tr_name"];
						item["tr_name"] = savedItem["tr_name"];
					}
					if (!String.IsNullOrEmpty(savedItem["tr_CV"]))
					{
						information.CV = savedItem["tr_CV"];
						item["tr_CV"] = savedItem["tr_CV"];
					}
				}
				if (saveJson)
				{
					data.Add(item);
				}
			}
			return data;
		}


		public static JsonArrayCollection ChipExplain(object source, JsonArrayCollection existingData, bool saveJson)
		{
			JsonArrayCollection data = new JsonArrayCollection();
			Contents.Text.ChipExplain.Format chipExplainFormat = (Contents.Text.ChipExplain.Format)source;
			string currentAssign;

			foreach (Contents.Text.ChipExplain.Information information in chipExplainFormat.Information)
			{
				JsonObjectCollection savedItem = default(JsonObjectCollection);
				JsonObjectCollection item = new JsonObjectCollection();
				if (information.Assign.GetType() == typeof(Contents.Text.Assign.TypeInt))
				{
					currentAssign = ((Contents.Text.Assign.TypeInt)information.Assign).Param.ToString(CultureInfo.InvariantCulture);
				}
				else
				{
					currentAssign = ((Contents.Text.Assign.TypeString)information.Assign).Param.Trim();
				}

				if (existingData != null)
				{
					int Count = existingData.Count;
					for (int idx = 0; idx < Count; idx++)
					{
						if (existingData[idx] != null &&
						    existingData[idx]["jp_explainShort"] == information.ExplainShort)
						{
							savedItem = (JsonObjectCollection)existingData[idx];
							existingData.RemoveAt(idx);
							break;
						}
					}
				}

				item["assign"] = currentAssign;
				item["jp_explainShort"] = information.ExplainShort;
				item["tr_explainShort"] = "";
				item["jp_explainLong"] = information.ExplainLong;
				item["tr_explainLong"] = "";

				if (savedItem != default(JsonObjectCollection))
				{
					if (!String.IsNullOrEmpty(savedItem["tr_explainShort"]))
					{
						information.ExplainShort = savedItem["tr_explainShort"];
						item["tr_explainShort"] = savedItem["tr_explainShort"];
					}
					if (!String.IsNullOrEmpty(savedItem["tr_explainLong"]))
					{
						information.ExplainLong = savedItem["tr_explainLong"];
						item["tr_explainLong"] = savedItem["tr_explainLong"];
					}
				}
				if (saveJson)
				{
					data.Add(item);
				}
			}
			return data;
		}

		public static JsonArrayCollection Name(object source, JsonArrayCollection existingData, bool saveJson)
		{
			JsonArrayCollection data = new JsonArrayCollection();
			Contents.Text.Name.Format textInformationFormat = (Contents.Text.Name.Format)source;
			if (textInformationFormat != null)
			{
				Contents.Text.Name.Information information = textInformationFormat.Information[0];
				if (information.Assign != null)
				{
					string currentAssign;
					Type infoType = information.Assign.GetType();
					foreach (Contents.Text.Name.Information innerInformation in textInformationFormat.Information)
					{
						JsonObjectCollection savedItem = default(JsonObjectCollection);
						currentAssign = "";
						JsonObjectCollection item = new JsonObjectCollection();
						if (infoType == typeof(Contents.Text.Assign.Type2Int))
						{
							Contents.Text.Assign.Type2Int assign = (Contents.Text.Assign.Type2Int)innerInformation.Assign;
							for (int i = 0; i < assign.Param.Length; i++)
								currentAssign += assign.Param[i].ToString(CultureInfo.InvariantCulture);
						}
						else if (infoType == typeof(Contents.Text.Assign.TypeInt))
						{
							Contents.Text.Assign.TypeInt assign = (Contents.Text.Assign.TypeInt)innerInformation.Assign;
							currentAssign = assign.Param.ToString(CultureInfo.InvariantCulture);
						}
						else if (infoType == typeof(Contents.Text.Assign.TypeString))
						{
							Contents.Text.Assign.TypeString assign = (Contents.Text.Assign.TypeString)innerInformation.Assign;
							currentAssign = assign.Param;
						}
						else if (infoType == typeof(Contents.Text.Assign.Type4Int))
						{
							Contents.Text.Assign.Type4Int assign = (Contents.Text.Assign.Type4Int)innerInformation.Assign;
							for (int i = 0; i < assign.Param.Length; i++)
								currentAssign += assign.Param[i].ToString(CultureInfo.InvariantCulture);
						}
						else
						{
							Console.Error.WriteLine("New assign format?");
						}

						if (existingData != null)
						{
							int Count = existingData.Count;
							for (int idx = 0; idx < Count; idx++)
							{
								if (existingData[idx] != null &&
								    existingData[idx]["jp_text"] == innerInformation.Name)
								{
									savedItem = (JsonObjectCollection)existingData[idx];
									existingData.RemoveAt(idx);
									break;
								}
							}
						}

						item["assign"] = currentAssign;
						item["jp_text"] = innerInformation.Name;
						item["tr_text"] = "";

						if (savedItem != default(JsonObjectCollection))
						{
							if (!String.IsNullOrEmpty(savedItem["tr_text"]))
							{
								innerInformation.Name = savedItem["tr_text"];
								item["tr_text"] = savedItem["tr_text"];
							}
						}
						if (saveJson)
						{
							data.Add(item);
						}
					}
				}
			}
			return data;
		}

		public static JsonArrayCollection Charged(object source, JsonArrayCollection existingData, bool saveJson)
		{
			JsonArrayCollection data = new JsonArrayCollection();
			Contents.Text.Charged.Format textInformationFormat = (Contents.Text.Charged.Format)source;
			if (textInformationFormat != null)
			{
				Contents.Text.Charged.Information information = textInformationFormat.Information[0];
				if (information.Assign != null)
				{
					string currentAssign;
					Type infoType = information.Assign.GetType();
					foreach (Contents.Text.Charged.Information innerInformation in textInformationFormat.Information)
					{
						JsonObjectCollection savedItem = default(JsonObjectCollection);
						currentAssign = "";
						JsonObjectCollection item = new JsonObjectCollection();
						if (infoType == typeof(Contents.Text.Assign.Type2Int))
						{
							Contents.Text.Assign.Type2Int assign = (Contents.Text.Assign.Type2Int)innerInformation.Assign;
							for (int i = 0; i < assign.Param.Length; i++)
								currentAssign += assign.Param[i].ToString(CultureInfo.InvariantCulture);
						}
						else if (infoType == typeof(Contents.Text.Assign.TypeInt))
						{
							Contents.Text.Assign.TypeInt assign = (Contents.Text.Assign.TypeInt)innerInformation.Assign;
							currentAssign = assign.Param.ToString(CultureInfo.InvariantCulture);
						}
						else if (infoType == typeof(Contents.Text.Assign.TypeString))
						{
							Contents.Text.Assign.TypeString assign = (Contents.Text.Assign.TypeString)innerInformation.Assign;
							currentAssign = assign.Param;
						}
						else if (infoType == typeof(Contents.Text.Assign.Type4Int))
						{
							Contents.Text.Assign.Type4Int assign = (Contents.Text.Assign.Type4Int)innerInformation.Assign;
							for (int i = 0; i < assign.Param.Length; i++)
								currentAssign += assign.Param[i].ToString(CultureInfo.InvariantCulture);
						}
						else
						{
							Console.Error.WriteLine("New assign format?");
						}

						if (existingData != null)
						{
							int Count = existingData.Count;
							for (int idx = 0; idx < Count; idx++)
							{
								if (existingData[idx] != null &&
								    existingData[idx]["jp_text"] == innerInformation.Name)
								{
									savedItem = (JsonObjectCollection)existingData[idx];
									existingData.RemoveAt(idx);
									break;
								}
							}
						}

						item["assign"] = currentAssign;
						item["jp_text"] = innerInformation.Name;
						item["tr_text"] = "";

						if (savedItem != default(JsonObjectCollection))
						{
							if (!String.IsNullOrEmpty(savedItem["tr_text"]))
							{
								innerInformation.Name = savedItem["tr_text"];
								item["tr_text"] = savedItem["tr_text"];
							}
						}
						if (saveJson)
						{
							data.Add(item);
						}
					}
				}
			}
			return data;
		}

		public static JsonArrayCollection NGWord(object source, JsonArrayCollection existingData, bool saveJson)
		{
			JsonArrayCollection data = new JsonArrayCollection();
			Contents.Text.NGWord.Format textInformationFormat = (Contents.Text.NGWord.Format)source;
			if (textInformationFormat != null)
			{
				Contents.Text.NGWord.Information information = textInformationFormat.Information[0];
				if (information.Assign != null)
				{
					string currentAssign;
					Type infoType = information.Assign.GetType();
					foreach (Contents.Text.NGWord.Information innerInformation in textInformationFormat.Information)
					{
						currentAssign = "";
						JsonObjectCollection item = new JsonObjectCollection();
						if (infoType == typeof(Contents.Text.Assign.Type2Int))
						{
							Contents.Text.Assign.Type2Int assign = (Contents.Text.Assign.Type2Int)innerInformation.Assign;
							for (int i = 0; i < assign.Param.Length; i++)
								currentAssign += assign.Param[i].ToString(CultureInfo.InvariantCulture);
						}
						else if (infoType == typeof(Contents.Text.Assign.TypeInt))
						{
							Contents.Text.Assign.TypeInt assign = (Contents.Text.Assign.TypeInt)innerInformation.Assign;
							currentAssign = assign.Param.ToString(CultureInfo.InvariantCulture);
						}
						else if (infoType == typeof(Contents.Text.Assign.TypeString))
						{
							Contents.Text.Assign.TypeString assign = (Contents.Text.Assign.TypeString)innerInformation.Assign;
							currentAssign = assign.Param;
						}
						else if (infoType == typeof(Contents.Text.Assign.Type4Int))
						{
							Contents.Text.Assign.Type4Int assign = (Contents.Text.Assign.Type4Int)innerInformation.Assign;
							for (int i = 0; i < assign.Param.Length; i++)
								currentAssign += assign.Param[i].ToString(CultureInfo.InvariantCulture);
						}
						else
						{
							Console.Error.WriteLine("New assign format?");
						}

						if (existingData != null)
						{
							int Count = existingData.Count;
							for (int idx = 0; idx < Count; idx++)
							{
								if (existingData[idx] != null &&
								    existingData[idx]["text"] == innerInformation.Text)
								{
									existingData.RemoveAt(idx);
									break;
								}
							}
						}

						item["assign"] = currentAssign;
						item["text"] = innerInformation.Text;

						if (saveJson)
						{
							data.Add(item);
						}
					}
				}
			}
			return data;
		}


		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Seraphy")]
		public static JsonArrayCollection SeraphyRoom(object source, JsonArrayCollection existingData, bool saveJson)
		{
			JsonArrayCollection data = new JsonArrayCollection();
			Contents.Text.SeraphyRoom.Format seraphyFormat = (Contents.Text.SeraphyRoom.Format)source;
			if (seraphyFormat != null)
			{
				Contents.Text.SeraphyRoom.Information information = seraphyFormat.Information[0];
				if (information.Assign != null)
				{
					foreach (Contents.Text.SeraphyRoom.Information innerInformation in seraphyFormat.Information)
					{
						JsonObjectCollection savedItem = default(JsonObjectCollection);
						JsonObjectCollection item = new JsonObjectCollection();

						if (existingData != null)
						{
							int Count = existingData.Count;
							for (int idx = 0; idx < Count; idx++)
							{
								if (existingData[idx] != null &&
								    existingData[idx]["jp_text"] == innerInformation.Text)
								{
									savedItem = (JsonObjectCollection)existingData[idx];
									existingData.RemoveAt(idx);
									break;
								}
							}
						}


						item["text_id"] = innerInformation.TextId;
						item["jp_text"] = innerInformation.Text;
						item["tr_text"] = "";

						if (savedItem != default(JsonObjectCollection))
						{
							if (!String.IsNullOrEmpty(savedItem["tr_text"]))
							{
								innerInformation.Text = savedItem["tr_text"];
								item["tr_text"] = savedItem["tr_text"];
							}
						}

						if (saveJson)
						{
							data.Add(item);
						}
					}
				}
			}
			return data;
		}

		public static JsonArrayCollection Item(object source, JsonArrayCollection existingData, bool saveJson)
		{
			JsonArrayCollection data = new JsonArrayCollection();
			Contents.Text.Item.Format textInformationFormat = (Contents.Text.Item.Format)source;
			if (textInformationFormat != null && textInformationFormat.Information != null)
			{
				Contents.Text.Item.Information information = textInformationFormat.Information[0];
				if (information.Assign != null)
				{
					string currentAssign;
					Type infoType = information.Assign.GetType();
					foreach (Contents.Text.Item.Information innerInformation in textInformationFormat.Information)
					{
						JsonObjectCollection savedItem = default(JsonObjectCollection);
						currentAssign = "";
						JsonObjectCollection item = new JsonObjectCollection();
						if (infoType == typeof(Contents.Text.Assign.Type2Int))
						{
							Contents.Text.Assign.Type2Int assign = (Contents.Text.Assign.Type2Int)innerInformation.Assign;
							for (int i = 0; i < assign.Param.Length; i++)
								currentAssign += assign.Param[i].ToString(CultureInfo.InvariantCulture);
						}
						else if (infoType == typeof(Contents.Text.Assign.TypeInt))
						{
							Contents.Text.Assign.TypeInt assign = (Contents.Text.Assign.TypeInt)innerInformation.Assign;
							currentAssign = assign.Param.ToString(CultureInfo.InvariantCulture);
						}
						else if (infoType == typeof(Contents.Text.Assign.TypeString))
						{
							Contents.Text.Assign.TypeString assign = (Contents.Text.Assign.TypeString)innerInformation.Assign;
							currentAssign = assign.Param;
						}
						else if (infoType == typeof(Contents.Text.Assign.Type4Int))
						{
							Contents.Text.Assign.Type4Int assign = (Contents.Text.Assign.Type4Int)innerInformation.Assign;
							for (int i = 0; i < assign.Param.Length; i++)
								currentAssign += assign.Param[i].ToString(CultureInfo.InvariantCulture);
						}
						else
						{
							Console.Error.WriteLine("New assign format?");
						}

						if (existingData != null)
						{
							int Count = existingData.Count;
							for (int idx = 0; idx < Count; idx++)
							{
								if (existingData[idx] != null &&
								    existingData[idx]["jp_text"] == innerInformation.Name.TrimEnd())
								{
									savedItem = (JsonObjectCollection)existingData[idx];
									existingData.RemoveAt(idx);
									break;
								}
							}
						}

						item["assign"] = currentAssign;
						item["jp_text"] = innerInformation.Name.TrimEnd();
						item["tr_text"] = "";
						item["jp_explain"] = innerInformation.Explain;
						item["tr_explain"] = "";

						if (savedItem != default(JsonObjectCollection))
						{
							if (!String.IsNullOrEmpty(savedItem["tr_text"]))
							{
								innerInformation.Name = savedItem["tr_text"];
								item["tr_text"] = savedItem["tr_text"];
							}
							if (!String.IsNullOrEmpty(savedItem["tr_explain"]))
							{
								innerInformation.Explain = savedItem["tr_explain"];
								item["tr_explain"] = savedItem["tr_explain"];
							}
						}
						if (saveJson)
						{
							data.Add(item);
						}
					}
				}

			}
			return data;
		}

		public static JsonArrayCollection Explain(object source, JsonArrayCollection existingData, bool saveJson)
		{
			JsonArrayCollection data = new JsonArrayCollection();
			Contents.Text.Explain.Format textInformationFormat = (Contents.Text.Explain.Format)source;
			if (textInformationFormat != null && textInformationFormat.Information != null)
			{
				Contents.Text.Explain.Information information = textInformationFormat.Information[0];
				if (information.Assign != null)
				{
					string currentAssign;
					Type infoType = information.Assign.GetType();
					foreach (Contents.Text.Explain.Information innerInformation in textInformationFormat.Information)
					{
						JsonObjectCollection savedItem = default(JsonObjectCollection);
						currentAssign = "";
						JsonObjectCollection item = new JsonObjectCollection();
						if (infoType == typeof(Contents.Text.Assign.Type2Int))
						{
							Contents.Text.Assign.Type2Int assign = (Contents.Text.Assign.Type2Int)innerInformation.Assign;
							for (int i = 0; i < assign.Param.Length; i++)
								currentAssign += assign.Param[i].ToString(CultureInfo.InvariantCulture);
						}
						else if (infoType == typeof(Contents.Text.Assign.TypeInt))
						{
							Contents.Text.Assign.TypeInt assign = (Contents.Text.Assign.TypeInt)innerInformation.Assign;
							currentAssign = assign.Param.ToString(CultureInfo.InvariantCulture);
						}
						else if (infoType == typeof(Contents.Text.Assign.TypeString))
						{
							Contents.Text.Assign.TypeString assign = (Contents.Text.Assign.TypeString)innerInformation.Assign;
							currentAssign = assign.Param;
						}
						else if (infoType == typeof(Contents.Text.Assign.Type4Int))
						{
							Contents.Text.Assign.Type4Int assign = (Contents.Text.Assign.Type4Int)innerInformation.Assign;
							for (int i = 0; i < assign.Param.Length; i++)
								currentAssign += assign.Param[i].ToString(CultureInfo.InvariantCulture);
						}
						else
						{
							Console.Error.WriteLine("New assign format?");
						}

						if (existingData != null)
						{
							int Count = existingData.Count;
							for (int idx = 0; idx < Count; idx++)
							{
								if (existingData[idx] != null &&
								    existingData[idx]["jp_text"] == innerInformation.Name)
								{
									savedItem = (JsonObjectCollection)existingData[idx];
									existingData.RemoveAt(idx);
									break;
								}
							}
						}

						item["assign"] = currentAssign;
						item["jp_text"] = innerInformation.Name;
						item["tr_text"] = "";
						item["jp_explain"] = innerInformation.Explain;
						item["tr_explain"] = "";

						if (savedItem != default(JsonObjectCollection))
						{
							if (!String.IsNullOrEmpty(savedItem["tr_text"]))
							{
								innerInformation.Name = savedItem["tr_text"];
								item["tr_text"] = savedItem["tr_text"];
							}
							if (!String.IsNullOrEmpty(savedItem["tr_explain"]))
							{
								innerInformation.Explain = savedItem["tr_explain"];
								item["tr_explain"] = savedItem["tr_explain"];
							}
						}
						if (saveJson)
						{
							data.Add(item);
						}
					}
				}
			}

			return data;
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
		public static JsonArrayCollection Awakening_Skill_Info(object source, JsonArrayCollection existingData, bool saveJson)
		{
			JsonArrayCollection data = new JsonArrayCollection();
			Contents.Text.ChipAwakeningInfos.Format skillAwakenFormat = (Contents.Text.ChipAwakeningInfos.Format)source;

			foreach (Contents.Text.ChipAwakeningInfos.Information information in skillAwakenFormat.Information)
			{
				JsonObjectCollection savedItem = default(JsonObjectCollection);
				JsonObjectCollection item = new JsonObjectCollection();

				if (existingData != null)
				{
					int Count = existingData.Count;
					for (int idx = 0; idx < Count; idx++)
					{
						if (existingData[idx] != null &&
						    existingData[idx]["jp_name"] == information.Name)
						{
							savedItem = (JsonObjectCollection)existingData[idx];
							existingData.RemoveAt(idx);
							break;
						}
					}
				}

                item["id"] = information.SkillId;
				item["jp_name"] = information.Name;
				item["tr_name"] = "";
				item["jp_desc"] = information.Description;
				item["tr_desc"] = "";

				if (savedItem != default(JsonObjectCollection))
				{
					if (!String.IsNullOrEmpty(savedItem["tr_desc"]))
					{
						information.Description = savedItem["tr_desc"];
						item["tr_desc"] = savedItem["tr_desc"];
					}
					if (!String.IsNullOrEmpty(savedItem["tr_name"]))
					{
						information.Name = savedItem["tr_name"];
						item["tr_name"] = savedItem["tr_name"];
					}
				}
				if (saveJson)
				{
					data.Add(item);
				}
			}
			return data;
		}

		public static JsonArrayCollection TowerQuest(object source, JsonArrayCollection existingData, bool saveJson)
		{
			JsonArrayCollection data = new JsonArrayCollection();
			Contents.Text.TowerQuest.Format towerFormat = (Contents.Text.TowerQuest.Format)source;
			if (towerFormat != null)
			{
				Contents.Text.TowerQuest.Information information = towerFormat.Information[0];
				if (information.Assign != null)
				{
					foreach (Contents.Text.TowerQuest.Information innerInformation in towerFormat.Information)
					{
						JsonObjectCollection savedItem = default(JsonObjectCollection);
						JsonObjectCollection item = new JsonObjectCollection();

						if (existingData != null)
						{
							int Count = existingData.Count;
							for (int idx = 0; idx < Count; idx++)
							{
								if (existingData[idx] != null &&
								    existingData[idx]["jp_text"] == innerInformation.Text)
								{
									savedItem = (JsonObjectCollection)existingData[idx];
									existingData.RemoveAt(idx);
									break;
								}
							}
						}

						item["jp_text"] = innerInformation.Text;
						item["tr_text"] = "";

						if (savedItem != default(JsonObjectCollection))
						{
							if (!String.IsNullOrEmpty(savedItem["tr_text"]))
							{
								innerInformation.Text = savedItem["tr_text"];
								item["tr_text"] = savedItem["tr_text"];
							}
						}

						if (saveJson)
						{
							data.Add(item);
						}
					}
				}
			}
			return data;
		}


		public static JsonArrayCollection RashArtsExplain(object source, JsonArrayCollection existingData, bool saveJson)
		{
			JsonArrayCollection data = new JsonArrayCollection();
			Contents.Text.RashArtsExplain.Format rashExplainFormat = (Contents.Text.RashArtsExplain.Format)source;
			string currentAssign;

			foreach (Contents.Text.RashArtsExplain.Information information in rashExplainFormat.Information)
			{
				JsonObjectCollection savedItem = default(JsonObjectCollection);
				JsonObjectCollection item = new JsonObjectCollection();
				if (information.Assign.GetType() == typeof(Contents.Text.Assign.TypeInt))
				{
					currentAssign = ((Contents.Text.Assign.TypeInt)information.Assign).Param.ToString(CultureInfo.InvariantCulture);
				}
				else
				{
					currentAssign = ((Contents.Text.Assign.TypeString)information.Assign).Param.Trim();
				}

				if (existingData != null)
				{
					int Count = existingData.Count;
					for (int idx = 0; idx < Count; idx++)
					{
						if (existingData[idx] != null &&
						    existingData[idx]["jp_explainShort"] == information.ExplainShort)
						{
							savedItem = (JsonObjectCollection)existingData[idx];
							existingData.RemoveAt(idx);
							break;
						}
					}
				}

				item["assign"] = currentAssign;
				item["jp_explainShort"] = information.ExplainShort;
				item["tr_explainShort"] = "";
				item["jp_explainLong"] = information.ExplainLong;
				item["tr_explainLong"] = "";
                item["flag"] = information.RashArtsFlg;

				if (savedItem != default(JsonObjectCollection))
				{
					if (!String.IsNullOrEmpty(savedItem["tr_explainShort"]))
					{
						information.ExplainShort = savedItem["tr_explainShort"];
						item["tr_explainShort"] = savedItem["tr_explainShort"];
					}
					if (!String.IsNullOrEmpty(savedItem["tr_explainLong"]))
					{
						information.ExplainLong = savedItem["tr_explainLong"];
						item["tr_explainLong"] = savedItem["tr_explainLong"];
					}
				}
				if (saveJson)
				{
					data.Add(item);
				}
			}
			return data;
		}
		public static JsonArrayCollection ChipAwakeningExplainTokens(object source, JsonArrayCollection existingData, bool saveJson)
		{
			JsonArrayCollection data = new JsonArrayCollection();

			Contents.Text.ChipAwakeningExplainTokens.Format tokensTitle = (Contents.Text.ChipAwakeningExplainTokens.Format)source;
			if (tokensTitle != null)
			{

				foreach (Contents.Text.ChipAwakeningExplainTokens.Information information in tokensTitle.Information)
				{
					int i = 0;
					for (int idx = 0; idx < information.Tokens.Length; idx++)
					{
						JsonObjectCollection savedItem = default(JsonObjectCollection);
						var param = information.Tokens[idx];
						JsonObjectCollection item = new JsonObjectCollection();

						if (existingData != null)
						{
							int Count = existingData.Count;
							for (int idy = 0; idy < Count; idy++)
							{
								if (existingData[idy] != null &&
								    existingData[idy]["jp_token"] == param)
								{
									savedItem = (JsonObjectCollection)existingData[idy];
									existingData.RemoveAt(idy);
									break;
								}
							}
						}

						item["jp_token"] = param;
						item["tr_token"] = "";
						if (savedItem != default(JsonObjectCollection))
						{
							if (!String.IsNullOrEmpty(savedItem["tr_token"]))
							{
								information.Tokens[idx] = savedItem["tr_token"];
								item["tr_token"] = savedItem["tr_token"];
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
