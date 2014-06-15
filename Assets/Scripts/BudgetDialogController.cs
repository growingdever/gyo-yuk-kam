using UnityEngine;
using System.Collections;

public class BudgetDialogController : MonoBehaviour, SliderReceiver
{

	public GameObject _sliderPrefab;

	GameManager _gameManager;
	BudgetManager _budgetManager;
	ArrayList _sliders;

	// Use this for initialization
	void Start ()
	{
		GameObject managerObject = GameObject.Find ("GameManager");
		_gameManager = managerObject.GetComponent<GameManager> ();

		_budgetManager = _gameManager.BudgetManager;
		_sliders = new ArrayList ();
		for (int i = 0; i < 7; i++) {
			GameObject clone = NGUITools.AddChild (gameObject, _sliderPrefab);
			_sliders.Add (clone);
			clone.transform.localPosition = new Vector3 (-400, 220 - 60 * i, 0);

			GameObject labelObject = clone.transform.FindChild ("Label").gameObject;
			UILabel label = labelObject.GetComponent<UILabel> ();
			label.text = _budgetManager.Titles [i];

			SliderSender sender = clone.GetComponent<SliderSender> ();
			sender.SetController (this);
			sender.Identifier.ID = i;

			UISlider slider = clone.GetComponent<UISlider> ();
			slider.value = (float)(_budgetManager.Point [i] / BudgetManager.Max);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void OnValueChanged (SliderSender sender)
	{
		int index = (int)sender.Identifier.ID;

		GameObject target = _sliders [index] as GameObject;
		UISlider slider = target.GetComponent<UISlider> ();

		_budgetManager.Point [index] = slider.value * BudgetManager.Max;
	}

	public void Finish ()
	{
		Destroy (gameObject);
	}
}
