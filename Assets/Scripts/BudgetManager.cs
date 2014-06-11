using UnityEngine;
using System.Collections;

public class BudgetManager {

	public enum Part {
		SchoolTime,
		NormalStudy,
		Stamina,
		Moral,
		Art,
		Aptitude,
		Holiday,
		Count,
	}

	public const double Max = 10.0;
	public readonly string[] Titles = {
		"학교 수업 시간",
		"국 영 수",
		"체육",
		"도덕",
		"미술",
		"적성 교육",
		"공휴일",
	};

	double[] _pointArray;
	public double[] Point {
		get {
			return _pointArray;
		}
	}

	public BudgetManager() {
		_pointArray = new double[(int)Part.Count];
		InitPoints();
	}

	public void InitPoints() {
		for( int i = 0; i < 7; i ++ ) {
			_pointArray[i] = 5;
		}
	}

	public ArrayList GetDeltaArray() {
		ArrayList list = new ArrayList();

		int mid = 5;
		double[,] coef = {
			{ -1, -0.5, 1, 0.5, 0.5, 2, 2 },
			{ 1, 0.5, -1, 0, 0.5, -2, -3 },
			{ 1, 0.5, -0.5, 0, 0.5, 0.5, 0 },
			{ -1, -0.5, 1, 0, 0, 0.5, 0.5 },
			{ -1, 0, 0.5, 1, 0.5, 0, 1 },
			{ -1, 0, 0.5, 0, 1, 1, 0.5 },
			{ 1, 0, -1, 0, 0, -0.5, -2 }
		};

		int totalStatus = coef.GetLength(0);

		double[] deltaArr = new double[totalStatus];
		for( int i = 0; i < totalStatus; i ++ ) {
			for( int j = 0; j < _pointArray.Length; j ++ ) {
				deltaArr[i] += coef[ i, j ] * (_pointArray[j] - mid);
			}
		}

		for( int i = 0; i < deltaArr.Length; i ++ ) {
			DeltaStatus delta = new DeltaStatus( Player.Status.GetStatusString(i), deltaArr[i] );
			list.Add( delta );
		}
        
        return list;
	}
}