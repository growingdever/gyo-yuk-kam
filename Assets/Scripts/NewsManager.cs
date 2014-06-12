using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;

public class NewsManager {

	ArrayList _newsDataList;
	public ArrayList NewsDataList {
		get {
			return _newsDataList;
		}
	}

	public NewsManager() {
		_newsDataList = new ArrayList();

		LoadNewsDataFromFiles();
	}

	void LoadNewsDataFromFiles() {
		DirectoryInfo dir = new DirectoryInfo("Assets/Resources/Data/News/");
		FileInfo[] info = dir.GetFiles("*.json");
		foreach (FileInfo f in info) {
			string onlyName = f.Name.Substring(0, f.Name.LastIndexOf("."));
			TextAsset fileContent = Resources.Load("Data/News/" + onlyName) as TextAsset;

			JsonData json = JsonMapper.ToObject(fileContent.text);
			
			News news = new News(json);
			_newsDataList.Add( news );
		}
	}

	public ArrayList GetSuitableNews(double[] statusArray) {
		ArrayList pRet = new ArrayList ();

		foreach (News news in _newsDataList) {
			Condition[] conditions = news.Conditions;

			int i;
			for( i = 0; i < conditions.Length; i ++ ) {
				Condition cond = conditions[i];
				if( statusArray[cond.Target] > cond.Value != cond.Cond ) {
					break;
				}
			}

			if( i == conditions.Length ) {
				pRet.Add( news );
			}
		}

		return pRet;
	}

	public class News {
		string _imagePath;
		public string ImagePath {
			get {
				return _imagePath;
			}
		}

		Condition[] _conditions;
		public Condition[] Conditions {
			get {
				return _conditions;
			}
		}

		public News(JsonData json) {
			_imagePath = (string)json["image"];

			JsonData conditions = json["conditions"];
			_conditions = new Condition[conditions.Count];
			for( int i = 0; i < conditions.Count; i ++ ) {
				JsonData data = conditions[i];
				Condition cond = new Condition( (int)data["target"], (int)data["value"], (bool)data["condition"] );
				_conditions[i] = cond;
			}
		}
	}

	public class Condition {
		int _target;
		public int Target {
			get {
				return _target;
			}
		}

		double _value;
		public double Value {
			get {
				return _value;
			}
		}

		bool _cond;
		public bool Cond {
			get {
				return _cond;
			}
		}

		public Condition(int target, int value, bool cond) {
			_target = target;
			_value = value;
			_cond = cond;
		}
	}
}
