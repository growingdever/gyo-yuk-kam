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
}