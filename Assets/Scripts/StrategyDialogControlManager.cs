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

		GameObject clone = NGUITools.AddChild( _unSelectedList.gameObject, _strategyLabelButtonPrefab );
		ButtonSender sender = clone.GetComponent<ButtonSender>();
		sender.SetController( this );
		sender.Identifier.ID = "Hello, World!";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClickedButton (ButtonSender sender)
	{
		Debug.Log( sender.Identifier.ID );
	}
}
