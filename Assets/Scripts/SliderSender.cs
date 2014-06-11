using UnityEngine;
using System.Collections;

public class SliderSender : MonoBehaviour {

	SliderReceiver _controller;
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

	public SliderSender() {
		_identifier = new Identifier();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetController(SliderReceiver controller) {
		_controller = controller;
	}

	public void OnValueChanged() {
		_controller.OnValueChanged( this );
	}
}
