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
	}
}
