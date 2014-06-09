using UnityEngine;
using System.Collections;

public class Identifier {
	private object _id;
	public object ID {
		get {
			return _id;
		}
		set {
			_id = value;
		}
	}
}
