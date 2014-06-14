using UnityEngine;
using System.Collections;

public class TitleShowCartoon : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnFinished() {
		StartCoroutine( ChangeScene() );
	}

	IEnumerator ChangeScene() {
		yield return new WaitForSeconds( 3.0f );
		Application.LoadLevel( 0 );
	}
}
