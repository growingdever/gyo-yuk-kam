using UnityEngine;
using System.Collections;

public class YeonimDialogController : MonoBehaviour {

	bool _isFail;
	bool _isClosed;
	public bool Closed {
		get {
			return _isClosed;
		}
	}

	// Use this for initialization
	void Start () {
		_isClosed = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClick() {
		if (_isFail) {
			// go to title scene
			Application.LoadLevel (1);	
		} else {
			// close dialog and play continually
			Close();
		}

		_isClosed = true;
	}

	public static YeonimDialogController BuildSuccess (GameObject parent) {
		GameObject prefab = Resources.Load ("DialogYeonimSuccess") as GameObject;
		GameObject clone = NGUITools.AddChild (parent, prefab);
		
		YeonimDialogController controller = clone.GetComponent<YeonimDialogController> ();
		controller._isFail = false;
		return controller;
	}

	public static YeonimDialogController BuildFail (GameObject parent) {
		GameObject prefab = Resources.Load ("DialogYeonimFail") as GameObject;
		GameObject clone = NGUITools.AddChild (parent, prefab);
		
		YeonimDialogController controller = clone.GetComponent<YeonimDialogController> ();
		controller._isFail = true;
		return controller;
	}

	public IEnumerator Show() {
		while (true) {
			if( _isClosed )
				break;
			yield return null;
		}
		
		Close ();
	}

	public void Close() {
		Destroy (gameObject);
	}
}
