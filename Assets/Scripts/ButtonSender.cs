using UnityEngine;
using System.Collections;

public class ButtonSender : MonoBehaviour {

	StrategyDialogControlManager _controller;
	Identifier _identifier;
	public Identifier Identifier {
		get {
			return _identifier;
		}
	}
	public object ID {
		get {
			return _identifier.ID;
		}
	}

	public ButtonSender() {
		_identifier = new Identifier();
	}

	// Use this for initialization
	void Start () {
//		_controller = _receiver.GetComponent<StrategyDialogControlManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetController(StrategyDialogControlManager controller) {
		_controller = controller;
	}

	public void OnClick() {
		_controller = GameObject.Find("StrategyDialog").GetComponent<StrategyDialogControlManager>();
		_controller.OnClickedButton( this );
	}
}
