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
		public const int IndexSatisfactionStudent = 0;
		public const int IndexSatisfactionParent = 1;
		public const int IndexIntelligence = 2;
		public const int IndexStamina = 3;
		public const int IndexMorality = 4;
		public const int IndexSpecialty = 5;
		public const int IndexStress = 6;
		public const int IndexTotal = 7;
		public double[] _variableArray;

		public Status() {
			_variableArray = new double[IndexTotal];

			for( int i = 0; i < IndexTotal; i ++ ) {
				_variableArray[i] = 3;
			}
			_variableArray[IndexSatisfactionParent] = 5;
			_variableArray[IndexSatisfactionStudent] = 5;
		}

		public static int GetIndex(string target) {
			switch( target ) {
			case "satisfactionStudent":
				return IndexSatisfactionStudent;
			case "satisfactionParent":
				return IndexSatisfactionParent;
			case "intelligence":
				return IndexIntelligence;
			case "stamina":
				return IndexStamina;
			case "morality":
				return IndexMorality;
			case "specialty":
				return IndexSpecialty;
			case "stress":
				return IndexStress;
			default:
				return -1;
			}
		}

		public float GetRatio( int index ) {
			return (float)(_variableArray [index] / Max);
		}

		public void ChangeStatus(DeltaStatus delta) {
			int index = delta.Target;
			_variableArray [index] += delta.Delta;
			for (int i = 0; i < IndexTotal; i ++) {
				if( _variableArray[i] > Max ) 
					_variableArray[i] = Max;
				else if( _variableArray[i] < 0 )
					_variableArray[i] = 0;
			}
		}
	}
}
