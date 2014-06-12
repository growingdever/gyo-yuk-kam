using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;

public class EndingManager : MonoBehaviour {

	public UISprite _image;
	public UILabel _label;


	ArrayList _endingDataArray;

	// Use this for initialization
	void Start () {
		// Get GameManager
		GameManager gManager = GameObject.Find("GameManager").GetComponent<GameManager>();

		// Check player status
		Player player = gManager.Player;

		// Load ending data from file
		_endingDataArray = new ArrayList();
		LoadDataFromFiles();

		ArrayList list = GetSuitableEnding( player.GetStatus()._variableArray );
		Ending ending = (Ending)list[ Random.Range( 0, list.Count ) ];
		_image.spriteName = ending.ImagePath;
		_label.text = ending.Description;
	}

	void LoadDataFromFiles() {
		DirectoryInfo dir = new DirectoryInfo("Assets/Resources/Data/Ending/");
		FileInfo[] info = dir.GetFiles("*.json");
		foreach (FileInfo f in info) {
			string onlyName = f.Name.Substring(0, f.Name.LastIndexOf("."));
			TextAsset fileContent = Resources.Load("Data/Ending/" + onlyName) as TextAsset;
			
			JsonData json = JsonMapper.ToObject(fileContent.text);

			Ending ending = new Ending(json);
			_endingDataArray.Add( ending );
		}
	}

	public ArrayList GetSuitableEnding(double[] statusArray) {
		ArrayList pRet = new ArrayList ();
		
		foreach (Ending ending in _endingDataArray) {
			Condition[] conditions = ending.Conditions;
			
			int i;
			for( i = 0; i < conditions.Length; i ++ ) {
				Condition cond = conditions[i];
				if( statusArray[cond.Target] > cond.Value != cond.Cond ) {
					break;
				}
			}
			
			if( i == conditions.Length ) {
				pRet.Add( ending );
			}
		}
		
		return pRet;
	}


	public class Ending {
		string _imagePath;
		public string ImagePath {
			get {
				return _imagePath;
			}
		}

		string _description;
		public string Description {
			get {
				return _description;
			}
		}
		
		Condition[] _conditions;
		public Condition[] Conditions {
			get {
				return _conditions;
			}
		}
		
		public Ending(JsonData json) {
			_imagePath = (string)json["image"];

			_description = (string)json["description"];
			
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
