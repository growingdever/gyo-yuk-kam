using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public const int MonthPerYear = 12;
	public int _startYear;
	public int _currMonth;

	private Player _player;
	private EventManager _eventManager;
	private ScheduleManager _scheduleManager;
	public ScheduleManager ScheduleManager {
		get {
			return _scheduleManager;
		}
	}
	private StrategyManager _strategyManager;
	public StrategyManager StrategyManager {
		get {
			return _strategyManager;
		}
	}

	public GameObject _uiRoot;
	public UIButton _mainButton1;
	public UIButton _mainButton2;
	public UIButton _mainButton3;
	public UIButton _mainButton4;

	public UILabel _calendarMonth;
	public UILabel _calendarDay;
	public UISlider _gaugeSatisfactionParent;
	public UISlider _gaugeSatisfactionStudent;
	
	public GameObject _eventDialogPrefab;
	private GameObject _currentEventDialog;

	public GameObject _budgetPrefab;
	public GameObject _strategyPrefab;

	private DeltaStatus[] _deltaStatusByBudget;

	private int[] DayOfMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };


	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	// Use this for initialization
	void Start () {
		_currMonth = 1;

		_calendarMonth.text = _currMonth + "";
		_calendarDay.text = 1 + "";

		_player = new Player();
		_eventManager = new EventManager ();
		_scheduleManager = new ScheduleManager();
		_strategyManager = new StrategyManager();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void OnClickNextMonth() {
		if( _currMonth % MonthPerYear == 1 ) {
			_mainButton2.enabled = false;
		}
		StartCoroutine ( "NextMonth", 1 );
	}
	public IEnumerator NextMonth(int delay) {

		int totalDay = DayOfMonth[_currMonth % MonthPerYear - 1];
		int eventDay = Random.Range( 1, totalDay + 1 );

		for( int i = 1; i <= totalDay; i ++ ) {
			if( i == eventDay ) {
				if( _eventManager.IsExistEvenyByMonth( _currMonth ) ) {
					GameObject dialog = NGUITools.AddChild( _uiRoot, _eventDialogPrefab );
					_currentEventDialog = dialog;
					
					DialogController dialogController = _currentEventDialog.GetComponent<DialogController>();
					dialogController.InGameEvent = _eventManager.GetEventByMonth( _currMonth );

					while(true) {
						if( dialogController.Closed ) break;
						yield return 0;
					}
				}
			}

			_calendarDay.text = i + "";
			yield return new WaitForSeconds( 0.05f );
		}

		_currMonth++;
		_calendarMonth.text = _currMonth + "";
		_calendarDay.text = 1 + "";
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

	public void OnClickStrategy() {
		NGUITools.AddChild (_uiRoot, _strategyPrefab);
	}

	public void FinishDecisionBudget(DeltaStatus[] values) {
		_deltaStatusByBudget = values;
	}
}
