using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
	public UISlider _gaugeIntelligence;
	public UISlider _gaugeStamina;
	public UISlider _gaugeMorality;
	public UISlider _gaugeSpecialty;
	public UISlider _gaugeStress;
	
	public GameObject _eventDialogPrefab;
	private GameObject _currentEventDialog;
	public bool _skipEventFlag;

	public GameObject _budgetPrefab;
	public GameObject _strategyPrefab;
	public GameObject _schedulePrefab;

	public GameObject _backSkyDay;
	public GameObject _backSkySunset;
	public GameObject _backSkyNight;


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

		UpdateGauge();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void UpdateGauge() {
		_gaugeSatisfactionParent.value = (float)(_player.GetStatus().SatisfactionParent / Player.Status.Max);
		_gaugeSatisfactionStudent.value = (float)(_player.GetStatus().SatisfactionStudent / Player.Status.Max);
		_gaugeIntelligence.value = (float)(_player.GetStatus().Int / Player.Status.Max);
		_gaugeStamina.value = (float)(_player.GetStatus().Stamina / Player.Status.Max);
		_gaugeMorality.value = (float)(_player.GetStatus().Morality / Player.Status.Max);
		_gaugeSpecialty.value = (float)(_player.GetStatus().Specialty / Player.Status.Max);
		_gaugeStress.value = (float)(_player.GetStatus().Stress / Player.Status.Max);
	}
	
	public void OnClickNextMonth() {
		int month = _currMonth % MonthPerYear;
		if( month == 3 || month == 9 ) {
			_mainButton1.enabled = false;
			_mainButton2.enabled = false;
			_mainButton3.enabled = false;
		}

		StartCoroutine ( "NextMonth", 0.05f );
	}
	public IEnumerator NextMonth(float delay) {
		int totalDay = DayOfMonth[_currMonth % MonthPerYear - 1];
		int eventDay = Random.Range( 1, totalDay + 1 );

		int[] endOfAnimationDay = new int[5];
		endOfAnimationDay [0] = 1;
		endOfAnimationDay [1] = (totalDay / 3) * 1;
		endOfAnimationDay [2] = (totalDay / 3) * 2;
		endOfAnimationDay [3] = (totalDay / 3) * 3;
		endOfAnimationDay [4] = totalDay;
		int currAnimation = 0;

		for( int i = 1; i <= totalDay; i ++ ) {
			if( i == endOfAnimationDay[ currAnimation ] && currAnimation <= 2 ) {
				int diff = endOfAnimationDay[ currAnimation + 1 ] - endOfAnimationDay[ currAnimation ];
				float duration = diff * delay;

				if( currAnimation == 0 ) {
					TweenAlpha.Begin (_backSkyDay, duration, 0);
					iTween.MoveTo (_backSkyDay, iTween.Hash ("X", 2, 
						"time", duration));
				} else if( currAnimation == 1 ) {
					TweenAlpha.Begin (_backSkySunset, duration, 0);
					iTween.MoveTo (_backSkySunset, iTween.Hash ("X", 2, 
						"time", duration));
				} else if( currAnimation == 2 ) {
					TweenAlpha.Begin (_backSkyNight, duration, 0);
					iTween.MoveTo (_backSkyNight, iTween.Hash ("X", 2, 
						"time", duration));
				}

				currAnimation++;
			}

			if( i == eventDay ) {
				if( _skipEventFlag )
					continue;

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
			yield return new WaitForSeconds( delay );
		}

		_currMonth++;
		int month = _currMonth % MonthPerYear;

		// calculate status
		if( month == 7 || month == 1 ) {
			_currMonth += 2;

			// change status
			ArrayList deltaList;
			deltaList = _strategyManager.GetDeltaArray();
			for( int i = 0; i < deltaList.Count; i ++ ) {
				DeltaStatus delta = deltaList[i] as DeltaStatus;
				_player.GetStatus().ChangeStatus( delta );
			}
			_strategyManager.ClearSelected();

			UpdateGauge();
		}

		// activate ui
		if (month == 7) {
			_mainButton1.enabled = true;
		}


		_calendarMonth.text = month + "";
		_calendarDay.text = 1 + "";

		// initialize background for next month
		_backSkyDay.transform.localPosition = new Vector3 (-1092, 223, 0);
		TweenAlpha.Begin (_backSkyDay, 0.01f, 1.0f);
		_backSkySunset.transform.localPosition = new Vector3 (-1092, 223, 0);
		TweenAlpha.Begin (_backSkySunset, 0.01f, 1.0f);
		_backSkyNight.transform.localPosition = new Vector3 (-1092, 223, 0);
		TweenAlpha.Begin (_backSkyNight, 0.01f, 1.0f);
	}

	public void OnClickBudget() {
		GameObject dialog = Instantiate( _budgetPrefab ) as GameObject;
		dialog.transform.parent = _uiRoot.transform;
		dialog.transform.localScale = new Vector3( 1, 1, 1 );
	}

	public void OnClickStrategy() {
		NGUITools.AddChild (_uiRoot, _strategyPrefab);
	}

	public void OnClickSchedule() {
		NGUITools.AddChild (_uiRoot, _schedulePrefab);
	}
}
