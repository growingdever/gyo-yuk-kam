using UnityEngine;
using System.Collections;

public class ButtonSender : MonoBehaviour
{

	ButtonReceiver _controller;
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

	public ButtonSender ()
	{
		_identifier = new Identifier ();
	}

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void SetController (ButtonReceiver controller)
	{
		_controller = controller;
	}

	public void OnClick ()
	{
		_controller.OnClickedButton (this);
	}
}
