using UnityEngine;
using System.Collections;

public class YeonimSuccess : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClickButtonExit() {
		// close dialog and play continually
		Destroy (gameObject);
	}
}
