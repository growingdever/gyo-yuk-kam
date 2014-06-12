using UnityEngine;
using System.Collections;

public class TitleStartButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClickStartButton() {
		Application.LoadLevel(1);
	}
}
