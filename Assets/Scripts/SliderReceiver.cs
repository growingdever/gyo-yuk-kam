using UnityEngine;
using System.Collections;

public interface SliderReceiver {
	void OnValueChanged(SliderSender sender);
}