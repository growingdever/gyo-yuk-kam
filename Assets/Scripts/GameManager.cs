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
	public UIButton _mainButton1;
	public UIButton _mainButton2;
	public UIButton _mainButton3;
	public UIButton _mainButton4;
	
	public GameObject _eventDialogPrefab;
	private GameObject _currentEventDialog;

	public GameObject _budgetPrefab;

	private DeltaStatus[] _deltaStatusByBudget;


	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

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
		if( _currMonth % MonthPerYear == 1 ) {
			_mainButton2.enabled = false;
		}
		StartCoroutine ("NextMonth", 1);
	}
	public IEnumerator NextMonth(int delay) {
		yield return new WaitForSeconds(delay);
		_currMonth++;
		_currTimeLabel.text = "현재 시간 : " + (_startYear + _currMonth / MonthPerYear) + "년 " + _currMonth % MonthPerYear + "월";

		if( _eventManager.IsExistEvenyByMonth( _currMonth ) ) {
			GameObject dialog = Instantiate( _eventDialogPrefab ) as GameObject;
			dialog.transform.parent = _uiRoot.transform;
			dialog.transform.localScale = new Vector3( 1, 1, 1 );
			_currentEventDialog = dialog;
			
			DialogController dialogController = _currentEventDialog.GetComponent<DialogController>();
			dialogController.InGameEvent = _eventManager.GetEventByMonth( _currMonth );
			
			UIButton button = dialog.GetComponent<UIButton>();
			EventDelegate eventDelegator = new EventDelegate();
			eventDelegator.Set( this, "OnClickDialog" );
			button.onClick.Add( eventDelegator );
		}
	}

	public void OnClickDialog() {
		DialogController dialogController = _currentEventDialog.GetComponent<DialogController>();
		dialogController.ShowChoice();
	}

	public void OnClickBudget() {
		GameObject dialog = Instantiate( _budgetPrefab ) as GameObject;
		dialog.transform.parent = _uiRoot.transform;
		dialog.transform.localScale = new Vector3( 1, 1, 1 );
	}

	public void FinishDecisionBudget(DeltaStatus[] values) {
		_deltaStatusByBudget = values;
	}
}
