using UnityEngine;
using System.Collections;

public class ScheduleDialogControlManager : MonoBehaviour, ButtonReceiver {

	public UIPanel _unSelectedList;
	public GameObject _labelButtonPrefab;

	ScheduleManager _scheduleManager;
	ScheduleManager.Schedule[] _selectedSchedules;
	ScheduleManager.Schedule _prevSelectedSchedule;

	GameObject[] _quaterNode;

	public ScheduleDialogControlManager() {

	}

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

		_selectedSchedules = new ScheduleManager.Schedule[4];
		_quaterNode = new GameObject[_selectedSchedules.Length];
		for (int i = 0; i < _quaterNode.Length; i ++) {
			GameObject finded = GameObject.Find("Month" + (i+1));
			_quaterNode[i] = finded;
		}

		for( int i = 0; i < _selectedSchedules.Length; i ++ ) {
			_selectedSchedules[i] = _scheduleManager.SelectedSchedules[i];
		}
		UpdateQuaterPart ();	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClickedButton (ButtonSender sender) {
		// add ui to panel
		int index = (int)sender.Identifier.ID;
		ScheduleManager.Schedule schedule = (ScheduleManager.Schedule)_scheduleManager.Schedules[index];
		_prevSelectedSchedule = schedule;
	}

	public void UpdateQuaterPart() {
		for( int i = 0; i < _selectedSchedules.Length; i ++ ) {
			ScheduleManager.Schedule schedule = _selectedSchedules[i];
			if( schedule == null ) 
				continue;

			// if that quater already has schedule, skip.
			if( _quaterNode[i].transform.childCount > 1 )
				continue;

			GameObject clone = NGUITools.AddChild( _quaterNode[i], _labelButtonPrefab );
			UIAnchor anchor = clone.GetComponent<UIAnchor> ();
			anchor.container = _quaterNode[i];

			GameObject labelObject = clone.transform.FindChild ("Label").gameObject;
			UILabel label = labelObject.GetComponent<UILabel>();
			label.text = schedule.Title;
			label.pivot = UIWidget.Pivot.Center;
		}
	}

	public void Finish() {
		for (int i = 0; i < _selectedSchedules.Length; i ++) {
			_scheduleManager.SelectedSchedules[i] = _selectedSchedules[i];
		}

		Destroy( gameObject );
	}

	public void OnClickQuater1() {
		_selectedSchedules [0] = _prevSelectedSchedule;
		UpdateQuaterPart ();
		_prevSelectedSchedule = null;
	}
	public void OnClickQuater2() {
		_selectedSchedules [1] = _prevSelectedSchedule;
		UpdateQuaterPart ();
		_prevSelectedSchedule = null;
	}
	public void OnClickQuater3() {
		_selectedSchedules [2] = _prevSelectedSchedule;
		UpdateQuaterPart ();
		_prevSelectedSchedule = null;
	}
	public void OnClickQuater4() {
		_selectedSchedules [3] = _prevSelectedSchedule;
		UpdateQuaterPart ();
		_prevSelectedSchedule = null;
	}
}
