using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class StrategyDialogControlManager : MonoBehaviour, ButtonReceiver {

	public UIPanel _unSelectedList;
	public UIPanel _selectedList;
	public GameObject _labelButtonPrefab;

	private StrategyManager _strategyManager;

	// Use this for initialization
	void Start () {
		GameObject managerObject = GameObject.Find("GameManager");
		GameManager gameManager = managerObject.GetComponent<GameManager>();
		_strategyManager = gameManager.StrategyManager;

		int size = _strategyManager.Strategies.Count;
		for (int i = 0; i < size; i ++) {
			StrategyManager.Strategy strategy = (StrategyManager.Strategy)_strategyManager.Strategies[i];

			GameObject clone = NGUITools.AddChild( _unSelectedList.gameObject, _labelButtonPrefab );

			UIAnchor anchor = clone.GetComponent<UIAnchor> ();
			anchor.container = _unSelectedList.gameObject;
			anchor.pixelOffset.Set( anchor.pixelOffset.x, -i * 20 );

			GameObject labelObject = clone.transform.FindChild ("Label").gameObject;
			UILabel label = labelObject.GetComponent<UILabel>();
			label.text = strategy.Title;

			ButtonSender sender = clone.GetComponent<ButtonSender>();
			sender.SetController( this );
			sender.Identifier.ID = i;
		}

		LinkedList<int> selected = _strategyManager.GetSelected ();
		size = selected.Count;
		for (int i = 0; i < size; i ++) {
			int index = selected.ElementAt(i);

			StrategyManager.Strategy strategy = (StrategyManager.Strategy)_strategyManager.Strategies[index];
			
			GameObject clone = NGUITools.AddChild( _selectedList.gameObject, _labelButtonPrefab );
			
			UIAnchor anchor = clone.GetComponent<UIAnchor> ();
			anchor.container = _selectedList.gameObject;
			anchor.pixelOffset.Set( anchor.pixelOffset.x, _selectedList.transform.childCount * 46 * -1 );
			
			GameObject labelObject = clone.transform.FindChild ("Label").gameObject;
			UILabel label = labelObject.GetComponent<UILabel>();
			label.text = strategy.Title;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClickedButton (ButtonSender sender) {
		// add ui to panel
		int index = (int)sender.Identifier.ID;
		StrategyManager.Strategy strategy = (StrategyManager.Strategy)_strategyManager.Strategies[index];

		GameObject clone = NGUITools.AddChild( _selectedList.gameObject, _labelButtonPrefab );
		
		UIAnchor anchor = clone.GetComponent<UIAnchor> ();
		anchor.container = _selectedList.gameObject;
		anchor.pixelOffset.Set( anchor.pixelOffset.x, _selectedList.transform.childCount * 46 * -1 );
		
		GameObject labelObject = clone.transform.FindChild ("Label").gameObject;
		UILabel label = labelObject.GetComponent<UILabel>();
		label.text = strategy.Title;

		// add selected strategy to manager
		_strategyManager.SelectStrategy (index);
	}

	public void Finish() {
		Destroy( gameObject );
	}
}