using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LitJson;
using System.IO;

public class EventManager
{

		ArrayList _eventArray;
		Dictionary<int, ArrayList> _eventMap;

		public EventManager ()
		{
				_eventArray = new ArrayList ();
				_eventMap = new Dictionary<int, ArrayList> ();
				for (int i = 1; i <= 12; i++) {
						_eventMap [i] = new ArrayList ();
				}

				LoadEventDataFromFile ();
		}

		void LoadEventDataFromFile ()
		{

				DirectoryInfo dir = new DirectoryInfo ("Assets/Resources/Data/Events/");
				FileInfo[] info = dir.GetFiles ("*.json");
				foreach (FileInfo f in info) {
						string onlyName = f.Name.Substring (0, f.Name.LastIndexOf ("."));
						TextAsset fileContent = Resources.Load ("Data/Events/" + onlyName) as TextAsset;

						JsonData json = JsonMapper.ToObject (fileContent.text);
						InGameEvent eventData = new InGameEvent (json, _eventMap, _eventArray.Count);
						_eventArray.Add (eventData);
				}
		}

		public bool IsExistEvenyByMonth (int month)
		{
				ArrayList list = _eventMap [month];
				return list.Count > 0;
		}

		public InGameEvent GetEventByMonth (int month)
		{
				ArrayList list = _eventMap [month];
				int at = Random.Range (0, list.Count);
				int eventPos = (int)list [at];
				InGameEvent eventData = _eventArray [eventPos] as InGameEvent;
				return eventData;
		}

		public class InGameEvent
		{
				private string _title;

				public string Title {
						get {
								return _title;
						}
				}

				private string _titleImagePath;

				public string TitleImagePath {
						get {
								return _titleImagePath;
						}
				}

				private string _content;

				public string Content {
						get {
								return _content;
						}
						set {
								_content = value;
						}
				}

				public Choice[] _choices;

				public Choice[] Choice {
						get {
								return _choices;
						}
				}

				public InGameEvent (JsonData json, Dictionary<int, ArrayList> map, int index)
				{
						JsonData timeArr = json ["time"];
						for (int i = 0; i < timeArr.Count; i++) {
								int month = (int)timeArr [i];
								map [month].Add (index);
						}

						_title = (string)json ["title"];
						_titleImagePath = (string)json ["title_image"];
						_content = (string)json ["content"];

						JsonData choices = (JsonData)json ["choices"];
						_choices = new Choice[choices.Count];
						for (int i = 0; i < choices.Count; i++) {
								_choices [i] = new Choice (choices [i]);
						}
				}
		}

		public class Choice
		{
				private string _title;

				public string Title {
						get {
								return _title;
						}
				}

				private Result[] _results;

				public Result[] Result {
						get {
								return _results;
						}
				}

				public Choice (JsonData json)
				{
						_title = (string)json ["title"];

						JsonData resultJson = json ["result"];
						_results = new Result[resultJson.Count];
						for (int i = 0; i < resultJson.Count; i++) {
								JsonData resultJsonData = resultJson [i];
								_results [i] = new Result (resultJsonData);
						}
				}
		}

		public class Result
		{
				private string _resultImage;

				public string ResultImagePath {
						get {
								return _resultImage;
						}
				}

				private string _resultContent;

				public string ResultContent {
						get {
								return _resultContent;
						}
				}

				private DeltaStatus[] _deltaStatus;

				public DeltaStatus[] DeltaStatus {
						get {
								return _deltaStatus;
						}
				}

				public Result (JsonData json)
				{
						_resultImage = (string)json ["title_image"];
						_resultContent = (string)json ["content"];

						JsonData deltaStatusDict = json ["change_status"];
						_deltaStatus = new DeltaStatus[deltaStatusDict.Count];
						for (int i = 0; i < deltaStatusDict.Count; i++) {
								JsonData deltaStatusItem = deltaStatusDict [i];
								string targetName = (string)deltaStatusItem ["status"];
								double delta = (double)deltaStatusItem ["delta"];
				
								_deltaStatus [i] = new DeltaStatus (targetName, delta);
						}
				}
		}
}
