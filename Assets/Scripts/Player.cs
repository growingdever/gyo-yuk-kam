using UnityEngine;
using System.Collections;

public class Player {

	private Status _status;

	public Player() {
		_status = new Status();
	}

	public class Status {
		double _satisfactionStudent;
		public double SatisfactionStudent { 
			get { return _satisfactionStudent; } 
			set { _satisfactionStudent = value; }
		}

		double _satisfactionParent;
		public double SatisfactionParent { 
			get { return _satisfactionParent; } 
			set { _satisfactionParent = value; }
		}

		double _intelligence;
		public double Int { 
			get { return _intelligence; } 
			set { _intelligence = value; }
		}

		double _stamina;
		public double Stamina { 
			get { return _stamina; } 
			set { _stamina = value; }
		}

		double _morality;
		public double Morality { 
			get { return _morality; } 
			set { _morality = value; }
		}

		double _specialty;
		public double Specialty { 
			get { return _specialty; } 
			set { _specialty = value; }
		}

		double _stress;
		public double Stress { 
			get { return _stress; } 
			set { _stress = value; }
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
