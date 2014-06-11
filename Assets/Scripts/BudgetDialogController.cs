using UnityEngine;
using System.Collections;

public class BudgetDialogController : MonoBehaviour, SliderReceiver {

	public GameObject _sliderPrefab;

	GameManager _gameManager;
	BudgetManager _budgetManager;
	ArrayList _sliders;

	// Use this for initialization
	void Start () {
		GameObject managerObject = GameObject.Find("GameManager");
		_gameManager = managerObject.GetComponent<GameManager>();

		_budgetManager = _gameManager.BudgetManager;
		_sliders = new ArrayList();
		for( int i = 0; i < 7; i ++ ) {
			GameObject clone = NGUITools.AddChild( gameObject, _sliderPrefab );
			_sliders.Add( clone );
			clone.transform.localPosition = new Vector3(-400, 220 - 60 * i, 0);

			GameObject labelObject = clone.transform.FindChild ("Label").gameObject;
			UILabel label = labelObject.GetComponent<UILabel>();
			label.text = _budgetManager.Titles[i];

			SliderSender sender = clone.GetComponent<SliderSender>();
			sender.SetController(this);
			sender.Identifier.ID = i;

			UISlider slider = clone.GetComponent<UISlider>();
			slider.value = (float)(_budgetManager.Point[i] / BudgetManager.Max);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnValueChanged(SliderSender sender) {
		int index = (int)sender.Identifier.ID;

		GameObject target = _sliders[index] as GameObject;
		UISlider slider = target.GetComponent<UISlider>();

		_budgetManager.Point[ index ] = slider.value * BudgetManager.Max;
	}

	public void Finish() {
		Destroy( gameObject );

//		int[] values = new int[7];
//		values[0] = (int)(_schoolTimeSlider.value * 10);
//		values[1] = (int)(_gookyoungsooSlider.value * 10);
//		values[2] = (int)(_athleticsSlider.value * 10);
//		values[3] = (int)(_moralitySlider.value * 10);
//		values[4] = (int)(_artSlider.value * 10);
//		values[5] = (int)(_aptitudeSlider.value * 10);
//		values[6] = (int)(_holidaySlider.value * 10);
//
//		int mid = 5;
//		double[,] coef = {
//			{ -1, 1, 1, -1, -1, -1, 1 },
//			{ -0.5, 0.5, 0.5, -0.5, 0, 0, 0 },
//			{ 1, -1, -0.5, 1, 0.5, 0.5, -1 },
//			{ 0.5, 0, 0, 0, 1, 0, 0 },
//			{ 0.5, 0.5, 0.5, 0, 0.5, 1, 0 },
//			{ 2, -2, 0.5, 0.5, 0, 1, -0.5 },
//			{ 2, -3, 0, 0.5, 1, 0.5, -2 }
//		};
//
//		double[] deltaArr = new double[7];
//		for( int i = 0; i < coef.Length; i ++ ) {
//			for( int j = 0; j < coef.GetLength(1); j ++ ) {
//				deltaArr[j] += coef[ i, j ] * (values[j] - mid);
//			}
//		}
//
//		DeltaStatus[] deltaStatusArray = new DeltaStatus[7];
//		for( int i = 0; i < 7; i ++ ) {
//			deltaStatusArray[i] = new DeltaStatus( Player.Status.GetStatusString(i), deltaArr[i] );
//		}
	}
}
