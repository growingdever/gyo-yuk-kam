using UnityEngine;
using System.Collections;

public class MyMessageBox : MonoBehaviour
{

		bool _isClosed;
		UILabel _labelTitle;
		UILabel _labelContent;
	
		// Use this for initialization
		void Start ()
		{
				_isClosed = false;
		}
	
		// Update is called once per frame
		void Update ()
		{
		
		}

		public static MyMessageBox Build (GameObject parent)
		{
				GameObject prefab = Resources.Load ("MessageBox") as GameObject;
				GameObject clone = NGUITools.AddChild (parent, prefab);
		
				MyMessageBox controller = clone.GetComponent<MyMessageBox> ();

				GameObject background = clone.transform.FindChild ("Background").gameObject;

				GameObject titleGameObject = background.transform.FindChild ("LabelTitle").gameObject;
				controller._labelTitle = titleGameObject.GetComponent<UILabel> ();
		
				GameObject contentGameObject = background.transform.FindChild ("LabelContent").gameObject;
				controller._labelContent = contentGameObject.GetComponent<UILabel> ();

				return controller;
		}

		public MyMessageBox SetTitle (string str)
		{
				_labelTitle.text = str;
				return this;
		}

		public MyMessageBox SetContent (string str)
		{
				_labelContent.text = str;
				return this;
		}

		public IEnumerator Show ()
		{
				while (true) {
						if (_isClosed)
								break;
						yield return null;
				}
		
				Close ();
		}

	
		public void Close ()
		{
				Destroy (gameObject);
		}

		public void OnClick ()
		{
				_isClosed = true;
		}

}
