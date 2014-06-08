using UnityEngine;
using System.Collections;

public class BudgetControlManager : MonoBehaviour {

	private GameManager _gameManager;

	public UISlider _schoolTimeSlider;
	public UISlider _gookyoungsooSlider;
	public UISlider _athleticsSlider;
	public UISlider _moralitySlider;
	public UISlider _artSlider;
	public UISlider _aptitudeSlider;
	public UISlider _holidaySlider;
	public UISlider _totalPointSlider;

	// Use this for initialization
	void Start () {
		GameObject managerObject = GameObject.Find("GameManager");
		_gameManager = managerObject.GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnValueChangedSchoolTime() {

	}

	public void Finish() {
		Destroy( gameObject );

		int[] values = new int[7];
		values[0] = (int)(_schoolTimeSlider.value * 10);
		values[1] = (int)(_gookyoungsooSlider.value * 10);
		values[2] = (int)(_athleticsSlider.value * 10);
		values[3] = (int)(_moralitySlider.value * 10);
		values[4] = (int)(_artSlider.value * 10);
		values[5] = (int)(_aptitudeSlider.value * 10);
		values[6] = (int)(_holidaySlider.value * 10);

		int mid = 5;
		double[,] coef = {
			{ -1, 1, 1, -1, -1, -1, 1 },
			{ -0.5, 0.5, 0.5, -0.5, 0, 0, 0 },
			{ 1, -1, -0.5, 1, 0.5, 0.5, -1 },
			{ 0.5, 0, 0, 0, 1, 0, 0 },
			{ 0.5, 0.5, 0.5, 0, 0.5, 1, 0 },
			{ 2, -2, 0.5, 0.5, 0, 1, -0.5 },
			{ 2, -3, 0, 0.5, 1, 0.5, -2 }
		};

		double[] deltaArr = new double[7];
		for( int i = 0; i < coef.Length; i ++ ) {
			for( int j = 0; j < coef.GetLength(i); j ++ ) {
				deltaArr[j] += coef[ i, j ] * (values[j] - mid);
			}
		}

		DeltaStatus[] deltaStatusArray = new DeltaStatus[7];
		for( int i = 0; i < 7; i ++ ) {
			deltaStatusArray[i] = new DeltaStatus( Player.Status.GetStatusString(i), deltaArr[i] );
		}

		_gameManager.FinishDecisionBudget( deltaStatusArray );
	}
}
