using UnityEngine;
using System.Collections;

public class NewsDialogController : MonoBehaviour {

	public UITexture _image;

	bool _isClosed;

	// Use this for initialization
	void Start () {
		_isClosed = false;
	}

	// Update is called once per frame
	void Update () {
	
	}

	public static NewsDialogController Build (GameObject parent, string image) {
		GameObject prefab = Resources.Load ("DialogNews") as GameObject;
		GameObject clone = NGUITools.AddChild (parent, prefab);

		NewsDialogController controller = clone.GetComponent<NewsDialogController> ();
		controller._image.mainTexture = Resources.Load ("Data/News/Newspaper/" + image) as Texture;
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

	public void OnClick() {
		_isClosed = true;
	}
}
