using UnityEngine;
using System.Collections;

public class MyNGUITool
{

		public static void SetLabelText (GameObject gameObject, string name, string content)
		{
				GameObject labelObject = gameObject.transform.FindChild (name).gameObject;
				UILabel label = labelObject.GetComponent<UILabel> ();
				label.text = content;
		}

		public static void SetLabelText (GameObject gameObject, string content)
		{
				UILabel label = gameObject.GetComponent<UILabel> ();
				label.text = content;
		}
}
