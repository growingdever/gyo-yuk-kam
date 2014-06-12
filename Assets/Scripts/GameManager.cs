using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour {

	public bool _skipEventFlag;
	public const int MonthPerYear = 12;
	public int _startYear;
	public int _currMonth;

	public int NumberOfTerm;
	public int IncumbencyYear;
	int _passedSemester;
	int _prevCheckedSemester;

	public float NextMonthSpeed;

	private Player _player;
	public Player Player {
		get {
			return _player;
		}
	}

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

	private BudgetManager _budgetManager;
	public BudgetManager BudgetManager {
		get {
			return _budgetManager;
		}
	}

	private NewsManager _newsManager;
	public NewsManager NewsManager {
		get {
			return _newsManager;
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
	UILabel _yeonimCounterLabel;
	
	public GameObject _eventDialogPrefab;
	private GameObject _currentEventDialog;

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
//		_currMonth = 3;

		_calendarMonth.text = CalculatedMonth() + "";
		_calendarDay.text = "1";

		GameObject yeonimUI = GameObject.Find ("Yeonim-num-label") as GameObject;
		_yeonimCounterLabel = yeonimUI.GetComponent<UILabel> ();
		_yeonimCounterLabel.text = "0";

		_player = new Player();
		_eventManager = new EventManager ();
		_scheduleManager = new ScheduleManager();
		_strategyManager = new StrategyManager();
		_budgetManager = new BudgetManager();
		_newsManager = new NewsManager ();

		UpdateGauge();

		StartCoroutine ("ShowNews", 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void UpdateGauge() {
		_gaugeSatisfactionParent.value = _player.GetStatus().GetRatio(Player.Status.IndexSatisfactionParent);
		_gaugeSatisfactionStudent.value = _player.GetStatus().GetRatio(Player.Status.IndexSatisfactionStudent);
		_gaugeIntelligence.value = _player.GetStatus().GetRatio(Player.Status.IndexIntelligence);
		_gaugeStamina.value = _player.GetStatus().GetRatio(Player.Status.IndexStamina);
		_gaugeMorality.value = _player.GetStatus().GetRatio(Player.Status.IndexMorality);
		_gaugeSpecialty.value = _player.GetStatus().GetRatio(Player.Status.IndexSpecialty);
		_gaugeStress.value = _player.GetStatus().GetRatio(Player.Status.IndexStress);
	}

	int CalculatedMonth() {
		if( _currMonth % MonthPerYear == 0 )
			return MonthPerYear;
		return _currMonth % MonthPerYear;
	}
	
	public void OnClickNextMonth() {
		int month = CalculatedMonth();
		if( month == 3 || month == 9 ) {
			_mainButton1.enabled = false;
			_mainButton2.enabled = false;
			_mainButton3.enabled = false;
		}

		StartCoroutine ( "NextMonth", NextMonthSpeed / 30.0f );
	}
	public IEnumerator NextMonth(float delay) {

		yield return StartCoroutine ( "PassingTime", delay );

		_currMonth++;
		int month = CalculatedMonth();

		// if semester is ended, add month more
		if( month == 7 || month == 1 ) {
			while( true ) {
				int tmp = CalculatedMonth();
				if( tmp == 9 || tmp == 3 )
                    break;
                _currMonth++;
			}
		}

		month = CalculatedMonth();

		// update calender ui
		_calendarMonth.text = month + "";
		_calendarDay.text = "1";

		// semester is ended!
		if( month == 3 || month == 9 ) {
			_passedSemester++;

			// change status
			do {
				ArrayList deltaList;

				// change by schedule

				// 1 year has passed
				if( month == 3 ) {
					// change by strategy
					deltaList = _strategyManager.GetDeltaArray();
					for( int i = 0; i < deltaList.Count; i ++ ) {
						DeltaStatus delta = deltaList[i] as DeltaStatus;
						_player.GetStatus().ChangeStatus( delta );
					}
                    _strategyManager.ClearSelected();
                    	
                    // change by budget
					deltaList = _budgetManager.GetDeltaArray();
					for( int i = 0; i < deltaList.Count; i ++ ) {
						DeltaStatus delta = deltaList[i] as DeltaStatus;
						_player.GetStatus().ChangeStatus( delta );
					}
                    _budgetManager.InitPoints();
                }
                
                UpdateGauge();
			} while( false );

			// activate and update ui
			month = CalculatedMonth();
			_mainButton1.enabled = true;
			if( month == 3 ) {
				_mainButton2.enabled = true;
            	_mainButton3.enabled = true;
			}
		}


		// if passed incumbency years, check satisfaction of parent for next term
		if( _passedSemester != _prevCheckedSemester && _passedSemester % (IncumbencyYear * 2) == 0 ) {
			int numOfTerm = _passedSemester / (IncumbencyYear * 2);
			if( numOfTerm == NumberOfTerm ) {
				Application.LoadLevel(2);
			}

			double needMoreSatisfaction = (numOfTerm - 1) * 1;

			YeonimDialogController controller;
			if( _player.GetStatus()._variableArray[ Player.Status.IndexSatisfactionParent ] >= 6 + needMoreSatisfaction ) {
				controller = YeonimDialogController.BuildSuccess( _uiRoot );
			} else {
				controller = YeonimDialogController.BuildFail( _uiRoot );
			}
			yield return StartCoroutine(controller.Show());

			_yeonimCounterLabel.text = "" + numOfTerm;
			_prevCheckedSemester = _passedSemester;
		}


		// show news
		ArrayList newsList = _newsManager.GetSuitableNewsIndex (_player.GetStatus ()._variableArray);
		if (newsList.Count > 0) {
			yield return StartCoroutine( "ShowNews", Random.Range( 1, newsList.Count ) );
		}


		// initialize background for next month
		_backSkyDay.transform.localPosition = new Vector3 (-1092, 223, 0);
		TweenAlpha.Begin (_backSkyDay, 0.01f, 1.0f);
		_backSkySunset.transform.localPosition = new Vector3 (-1092, 223, 0);
		TweenAlpha.Begin (_backSkySunset, 0.01f, 1.0f);
		_backSkyNight.transform.localPosition = new Vector3 (-1092, 223, 0);
		TweenAlpha.Begin (_backSkyNight, 0.01f, 1.0f);
	}

	IEnumerator PassingTime(float delay) {
		int totalDay = DayOfMonth[CalculatedMonth() - 1];
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
				
				if( _eventManager.IsExistEvenyByMonth( CalculatedMonth() ) ) {
					GameObject dialog = NGUITools.AddChild( _uiRoot, _eventDialogPrefab );
					_currentEventDialog = dialog;
					
					EventDialogController dialogController = _currentEventDialog.GetComponent<EventDialogController>();
					dialogController.InGameEvent = _eventManager.GetEventByMonth( CalculatedMonth() );
					
					while(true) {
						if( dialogController.Closed ) break;
						yield return 0;
					}
					
					dialogController.UpdatePlayerStatus( _player );
					UpdateGauge();
				}
			}
			
			_calendarDay.text = i + "";
			yield return new WaitForSeconds( delay );
		}
	}

	IEnumerator ShowNews(int index) {	
		NewsManager.News news = (NewsManager.News)_newsManager.NewsDataList[index];
		yield return StartCoroutine( NewsDialogController.Build(_uiRoot, news).Show () );
	}

	public void OnClickBudget() {
		NGUITools.AddChild( _uiRoot, _budgetPrefab );
	}

	public void OnClickStrategy() {
		NGUITools.AddChild (_uiRoot, _strategyPrefab);
	}

	public void OnClickSchedule() {
		NGUITools.AddChild (_uiRoot, _schedulePrefab);
	}

	public void OnClickPresentCondition() {

	}
}
