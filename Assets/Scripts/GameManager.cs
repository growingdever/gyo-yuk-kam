using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public const int MonthPerYear = 12;
	public int _startYear;
	public int _currMonth;
	public UILabel _currTimeLabel;

	private Player _player;
	private EventManager _eventManager;

	public GameObject _uiRoot;
	public GameObject _dialogPrefab;
	private GameObject _currentDialog;

	// Use this for initialization
	void Start () {
		_currMonth = 1;
		_currTimeLabel.text = "현재 시간 : " + (_startYear + _currMonth / MonthPerYear) + "년 " + _currMonth % MonthPerYear + "월";

		_player = new Player();
		_eventManager = new EventManager ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void OnClickNextMonth() {
		StartCoroutine ("NextMonth", 1);
	}
	public IEnumerator NextMonth(int delay) {
		yield return new WaitForSeconds(delay);
		_currMonth++;
		_currTimeLabel.text = "현재 시간 : " + (_startYear + _currMonth / MonthPerYear) + "년 " + _currMonth % MonthPerYear + "월";


		GameObject dialog = Instantiate( _dialogPrefab ) as GameObject;
		dialog.transform.parent = _uiRoot.transform;
		dialog.transform.localScale = new Vector3( 1, 1, 1 );
		_currentDialog = dialog;

		DialogController dialogController = _currentDialog.GetComponent<DialogController>();
		dialogController.InGameEvent = _eventManager.GetEventByMonth( _currMonth );

		UIButton button = dialog.GetComponent<UIButton>();
		EventDelegate eventDelegator = new EventDelegate();
		eventDelegator.Set( this, "OnClickDialog" );
		button.onClick.Add( eventDelegator );
	}

	public void OnClickDialog() {
		DialogController dialogController = _currentDialog.GetComponent<DialogController>();
		dialogController.ShowChoice();
	}
}
