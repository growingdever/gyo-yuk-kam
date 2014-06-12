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

	public ArrayList GetSuitableNewsIndex(double[] statusArray) {
		ArrayList pRet = new ArrayList ();

		for( int i = 0; i < _newsDataList.Count; i ++ ) {
			News news = (News)_newsDataList[i];
			Condition[] conditions = news.Conditions;

			int j;
			for( j = 0; j < conditions.Length; j ++ ) {
				Condition cond = conditions[j];
				if( statusArray[cond.Target] > cond.Value != cond.Cond ) {
					break;
				}
			}

			if( j == conditions.Length ) {
				pRet.Add( i );
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
}
