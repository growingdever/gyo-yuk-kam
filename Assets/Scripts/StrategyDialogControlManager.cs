using UnityEngine;
using System.Collections;

public class StrategyDialogControlManager : MonoBehaviour, ButtonReceiver {

	public UIPanel _unSelectedList;
	public UIPanel _selectedList;
	public GameObject _strategyLabelButtonPrefab;

	private StrategyManager _strategyManager;

	// Use this for initialization
	void Start () {
		GameObject managerObject = GameObject.Find("GameManager");
		GameManager gameManager = managerObject.GetComponent<GameManager>();
		_strategyManager = gameManager.StrategyManager;

		int size = _strategyManager.Strategies.Count;
		for (int i = 0; i < size; i ++) {
			StrategyManager.Strategy strategy = (StrategyManager.Strategy)_strategyManager.Strategies[i];
			Debug.Log( strategy.Title );

			GameObject clone = NGUITools.AddChild( _unSelectedList.gameObject, _strategyLabelButtonPrefab );
			MyNGUITool.SetLabelText( clone, "Label", strategy.Title );

			ButtonSender sender = clone.GetComponent<ButtonSender>();
			sender.SetController( this );
			sender.Identifier.ID = i;
			
			UIAnchor anchor = clone.GetComponent<UIAnchor> ();
			anchor.container = _unSelectedList.gameObject;
			anchor.pixelOffset.Set( anchor.pixelOffset.x, i * 24 );
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClickedButton (ButtonSender sender) {
		Debug.Log( sender.Identifier.ID );
	}

	public void Finish() {
		Destroy( gameObject );
	}
}