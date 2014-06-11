using UnityEngine;
using System.Collections;

public class Player {

	private Status _status;

	public Player() {
		_status = new Status();
	}

	public Status GetStatus() {
		return _status;
	}

	public class Status {
		public const double Max = 10.0f;

		double _satisfactionStudent;
		public double SatisfactionStudent { 
			get { return _satisfactionStudent; } 
		}

		double _satisfactionParent;
		public double SatisfactionParent { 
			get { return _satisfactionParent; } 
		}

		double _intelligence;
		public double Int { 
			get { return _intelligence; } 
		}

		double _stamina;
		public double Stamina { 
			get { return _stamina; } 
		}

		double _morality;
		public double Morality { 
			get { return _morality; } 
		}

		double _specialty;
		public double Specialty { 
			get { return _specialty; } 
		}

		double _stress;
		public double Stress { 
			get { return _stress; } 
		}

		public Status() {
			_satisfactionParent = 5;
			_satisfactionStudent = 5;
			_intelligence = 3;
			_stamina = 3;
			_morality = 3;
            _specialty = 3;
            _stress = 3;
		}

		// this function will cause performance degradation because too many compare string
		// but use readability....
		// should fix to serializable data later
		public void ChangeStatus(DeltaStatus delta) {
			switch( delta.Target ) {
			case "satisfactionStudent":
				_satisfactionStudent += delta.Delta;
				break;
			case "satisfactionParent":
				_satisfactionParent += delta.Delta;
				break;
			case "intelligence":
				_intelligence += delta.Delta;
				break;
			case "stamina":
				_stamina += delta.Delta;
				break;
			case "morality":
				_morality += delta.Delta;
				break;
			case "specialty":
				_specialty += delta.Delta;
				break;
			case "stress":
				_stress += delta.Delta;
				break;
			}

			if( _satisfactionParent > Max ) _satisfactionParent = Max;
			else if( _satisfactionParent < 0 ) _satisfactionParent = 0;

			if( _satisfactionStudent > Max ) _satisfactionStudent = Max;
			else if( _satisfactionStudent < 0 ) _satisfactionStudent = 0;

			if( _intelligence > Max ) _intelligence = Max;
			else if( _intelligence < 0 ) _intelligence = 0;

			if( _stamina > Max ) _stamina = Max;
			else if( _stamina < 0 ) _stamina = 0;

			if( _morality > Max ) _morality = Max;
			else if( _morality < 0 ) _morality = 0;

			if( _specialty > Max ) _specialty = Max;
			else if( _specialty < 0 ) _specialty = 0;

			if( _stress > Max ) _stress = Max;
			else if( _stress < 0 ) _stress = 0;
		}

		public static string GetStatusString(int type) {
			switch( type ) {
			case 0:
				return "satisfactionStudent";
			case 1:
				return "satisfactionParent";
			case 2:
				return "intelligence";
			case 3:
				return "stamina";
			case 4:
				return "morality";
			case 5:
				return "specialty";
			case 6:
				return "Stress";
			default:
				return "";
			}
		}
	}
}
