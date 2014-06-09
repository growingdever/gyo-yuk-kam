using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LitJson;
using System.IO;

public class ScheduleManager {

	ArrayList _scheduleArray;
	public ArrayList Schedules {
		get {
			return _scheduleArray;
		}
	}

	public ScheduleManager() {
		_scheduleArray = new ArrayList();
		LoadScheduleDataFromFile ();
	}

	void LoadScheduleDataFromFile() {
		DirectoryInfo dir = new DirectoryInfo("Assets/Resources/Data/Schedules/");
		FileInfo[] info = dir.GetFiles("*.json");
		foreach (FileInfo f in info) {
			string onlyName = f.Name.Substring(0, f.Name.LastIndexOf("."));
			TextAsset fileContent = Resources.Load("Data/Schedules/" + onlyName) as TextAsset;

			JsonData json = JsonMapper.ToObject(fileContent.text);
			Schedule scheduleData = new Schedule(json);
			_scheduleArray.Add( scheduleData );
		}
	}

	public class Schedule {
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
		
		public Schedule(JsonData json) {
			_title = (string)json["title"];
			
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
