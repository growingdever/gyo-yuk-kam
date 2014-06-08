using System;

public class DeltaStatus
{
	string _target;
	public string Target {
		get {
			return _target;
		}
	}
	double _delta;
	public double Delta {
		get {
			return _delta;
		}
	}

	public DeltaStatus (string target, double delta)
	{
		_target = target;
		_delta = delta;
	}
}