using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LitJson;
using System.IO;

public class StrategyManager {

	ArrayList _strategyArray;
	public ArrayList Strategies {
		get {
			return _strategyArray;
		}
	}

	public StrategyManager() {
		_strategyArray = new ArrayList();
		LoadStrategyDataFromFile ();
	}

	void LoadStrategyDataFromFile() {
		DirectoryInfo dir = new DirectoryInfo("Assets/Resources/Data/Strategies/");
		FileInfo[] info = dir.GetFiles("*.json");
		foreach (FileInfo f in info) {
			string onlyName = f.Name.Substring(0, f.Name.LastIndexOf("."));
			TextAsset fileContent = Resources.Load("Data/Strategies/" + onlyName) as TextAsset;

			JsonData json = JsonMapper.ToObject(fileContent.text);
			Strategy strategy = new Strategy(json);
			_strategyArray.Add( strategy );
		}
	}

	public class Strategy {
		private string _title;
		public string Title {
			get {
				return _title;
			}
		}
		
		private DeltaStatus[] _deltaStatus;
		public DeltaStatus[] DeltaStatus {
			get {
				return _deltaStatus;
			}
		}
		
		public Strategy(JsonData json) {
			_title = (string)json["title"];

			IDictionary tdictionary = json as IDictionary;
			if(tdictionary.Contains("socket"))
			{

			}
			
			JsonData deltaStatusDict = json["change_status"];
			_deltaStatus = new DeltaStatus[deltaStatusDict.Count];
			for( int i = 0; i < deltaStatusDict.Count; i ++ ) {
				JsonData deltaStatusItem = deltaStatusDict[i];
				string targetName = (string)deltaStatusItem["status"];
                double delta = (double)deltaStatusItem["delta"];
                _deltaStatus[i] = new DeltaStatus( targetName, delta );
            }
		}
	}
}
