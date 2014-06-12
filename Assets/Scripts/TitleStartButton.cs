using UnityEngine;
using System.Collections;

public class TitleStartButton : MonoBehaviour {

	public UITexture _tutorialTexture;
	int _curr;

	// Use this for initialization
	void Start () {
		NGUITools.SetActive (_tutorialTexture.gameObject, false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClickStartButton() {
		NextTutorial ();
	}

	public void NextTutorial() {
		NGUITools.SetActive (_tutorialTexture.gameObject, true);

		_curr++;
		if (_curr > 5) {
			Application.LoadLevel (1);
		}

		Texture tex = Resources.Load ("Images/Tu" + _curr) as Texture;
		_tutorialTexture.mainTexture = tex;
	}
}
