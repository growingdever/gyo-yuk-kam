using UnityEngine;
using System.Collections;

public interface ButtonReceiver
{
	void OnClickedButton (ButtonSender sender);
}