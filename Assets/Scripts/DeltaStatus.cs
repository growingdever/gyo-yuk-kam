using System;

public class DeltaStatus
{
	int _target;

	public int Target {
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
		_target = Player.Status.GetIndex (target);
		_delta = delta;
	}

	public DeltaStatus (int index, double delta)
	{
		_target = index;
		_delta = delta;
	}
}