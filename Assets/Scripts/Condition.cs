public class Condition {
	int _target;
	public int Target {
		get {
			return _target;
		}
	}

	double _value;
	public double Value {
		get {
			return _value;
		}
	}

	bool _cond;
	public bool Cond {
		get {
			return _cond;
		}
	}

	public Condition(int target, int value, bool cond) {
		_target = target;
		_value = value;
		_cond = cond;
	}
}