using UnityEngine;
using System.Collections;

public class EndingLabelTransition : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (Tweening ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Tweening() {
		TweenAlpha.Begin (gameObject, 3.0f, 1.0f);
		yield return new WaitForSeconds (3.0f);

		TweenAlpha.Begin (gameObject, 3.0f, 0.0f);
		yield return new WaitForSeconds (3.0f);

		Application.LoadLevel (0);
	}
}
