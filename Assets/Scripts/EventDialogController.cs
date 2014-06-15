using UnityEngine;
using System.Collections;

public class EventDialogController : MonoBehaviour
{
	
		public UITexture _image;
		public UILabel _title;
		public UILabel _content;
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
		EventManager.Result _result;

		public EventManager.Result Result {
				get {
						return _result;
				}
		}

		bool _isClosed;

		public bool Closed {
				get {
						return _isClosed;
				}
		}

		// Use this for initialization
		void Start ()
		{
				_isClosed = false;

				NGUITools.SetActive (_choice1, false);
				NGUITools.SetActive (_choice2, false);

				_title.text = _eventData.Title;
				_content.text = _eventData.Content;

				string path = "Data/Events/EventIMGs/" + _eventData.TitleImagePath;
				Texture tex = Resources.Load (path) as Texture;
				_image.mainTexture = tex;
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		public void OnClick ()
		{
				if (_content.enabled == true) {
						ShowChoice ();
				}
		}

		public void SetEvent (EventManager.InGameEvent eventData)
		{
				_eventData = eventData;
		}

		public void ShowChoice ()
		{
				if (_selectedChoice != null) {
						Destroy (gameObject);
						_isClosed = true;
				}

				_content.enabled = false;

				NGUITools.SetActive (_choice1, true);
				NGUITools.SetActive (_choice2, true);

				MyNGUITool.SetLabelText (_choice1, "Label", _eventData.Choice [0].Title);
				MyNGUITool.SetLabelText (_choice2, "Label", _eventData.Choice [1].Title);
		}

		// below code should be changed.
		// because it isn't flexiable....
		// if there are only 2 choice, it doesn't matter.
		public void OnClickChoice1 ()
		{
				_selectedChoice = _eventData.Choice [0];
				UpdateDialogStateResult ();
		}

		public void OnClickChoice2 ()
		{
				_selectedChoice = _eventData.Choice [1];
				UpdateDialogStateResult ();
		}

		void UpdateDialogStateResult ()
		{
				NGUITools.SetActive (_choice1, false);
				NGUITools.SetActive (_choice2, false);
				_content.enabled = true;

				EventManager.Result result = _selectedChoice.Result [Random.Range (0, _selectedChoice.Result.Length)];
				_title.text = _selectedChoice.Title;
				_content.text = result.ResultContent;
				_result = result;

				string path = "Data/Events/EventIMGs/" + result.ResultImagePath;
				Texture tex = Resources.Load (path) as Texture;
				_image.mainTexture = tex;
		}

		public void UpdatePlayerStatus (Player player)
		{
				DeltaStatus[] deltaArray = _result.DeltaStatus;
				for (int i = 0; i < deltaArray.Length; i++) {
						player.GetStatus ().ChangeStatus (deltaArray [i]);
				}
		}
}
