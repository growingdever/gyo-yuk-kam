using UnityEngine;
using System.Collections;

public class DialogController : MonoBehaviour {

	public GameObject _target;
	public GameObject _image;
	public GameObject _title;
	public GameObject _content;
	public GameObject _choice1;
	public GameObject _choice2;

	private EventManager.InGameEvent _eventData;
	public EventManager.InGameEvent InGameEvent {
		get {
			return _eventData;
		}
		set {
			_eventData = value;
		}
	}
	private EventManager.Choice _selectedChoice;

	// Use this for initialization
	void Start () {
		NGUITools.SetActive( _choice1, false );
		NGUITools.SetActive( _choice2, false );

		MyNGUITool.SetLabelText (_title, _eventData.Title);
		MyNGUITool.SetLabelText (_content, _eventData.Content);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClick() {
		if( NGUITools.GetActive( _content ) == true ) {
			ShowChoice();
		}
	}

	public void SetEvent(EventManager.InGameEvent eventData) {
		_eventData = eventData;
	}

	public void ShowChoice() {
		if (_selectedChoice != null) {
			Destroy( _target );
		}

		NGUITools.SetActive( _content, false );

		NGUITools.SetActive( _choice1, true );
		NGUITools.SetActive( _choice2, true );

		MyNGUITool.SetLabelText (_choice1, "Label", _eventData.Choice [0].Title);
		MyNGUITool.SetLabelText (_choice2, "Label", _eventData.Choice [1].Title);
	}

	// below code should be changed.
	// because it isn't flexiable....
	// if there are only 2 choice, it doesn't matter.
	public void OnClickChoice1() {
		_selectedChoice = _eventData.Choice[0];
		UpdateDialogStateResult ();
	}
	public void OnClickChoice2() {
		_selectedChoice = _eventData.Choice[1];
		UpdateDialogStateResult ();
	}

	void UpdateDialogStateResult() {
		NGUITools.SetActive (_choice1, false);
		NGUITools.SetActive (_choice2, false);
		NGUITools.SetActive (_content, true);

		EventManager.Result result = _selectedChoice.Result[Random.Range(0, _selectedChoice.Result.Length)];
		MyNGUITool.SetLabelText (_title, _selectedChoice.Title);
		MyNGUITool.SetLabelText (_content, result.ResultContent);


		// send selected choice to manager
	}
}
