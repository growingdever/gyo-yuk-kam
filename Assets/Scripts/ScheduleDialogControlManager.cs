using UnityEngine;
using System.Collections;

public class ScheduleDialogControlManager : MonoBehaviour, ButtonReceiver {

	public UIPanel _unSelectedList;
	public GameObject _labelButtonPrefab;

	ScheduleManager _scheduleManager;

	// Use this for initialization
	void Start () {
		GameObject managerObject = GameObject.Find("GameManager");
		GameManager gameManager = managerObject.GetComponent<GameManager>();
		_scheduleManager = gameManager.ScheduleManager;

		int size = _scheduleManager.Schedules.Count;
		for (int i = 0; i < size; i ++) {
			ScheduleManager.Schedule schedule = (ScheduleManager.Schedule)_scheduleManager.Schedules[i];

			GameObject clone = NGUITools.AddChild( _unSelectedList.gameObject, _labelButtonPrefab );
			UIAnchor anchor = clone.GetComponent<UIAnchor> ();
			anchor.container = _unSelectedList.gameObject;
			anchor.pixelOffset.Set( anchor.pixelOffset.x, -i * 20 );

			GameObject labelObject = clone.transform.FindChild ("Label").gameObject;
			UILabel label = labelObject.GetComponent<UILabel>();
			label.text = schedule.Title;

			ButtonSender sender = clone.GetComponent<ButtonSender>();
			sender.SetController( this );
			sender.Identifier.ID = i;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClickedButton (ButtonSender sender) {
		// add ui to panel
		int index = (int)sender.Identifier.ID;
		ScheduleManager.Schedule schedule = (ScheduleManager.Schedule)_scheduleManager.Schedules[index];

		Debug.Log (schedule.Title);
	}

	public void Finish() {

	}
}
