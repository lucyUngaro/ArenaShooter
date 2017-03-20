using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTask : Task {

	private readonly GameObject _boss; 
	private readonly Vector3 _initialScale;
	private readonly Vector3 _finalScale;


	public ScaleTask (GameObject b, Vector3 s)
	{
		_boss = b;  
		_initialScale = b.transform.localScale;
		_finalScale = s;
	}

	protected override void Init()
	{
		ScaleObj ();

	}

	public void ScaleObj()
	{
		if (_boss.transform.localScale != _finalScale) {
			_boss.transform.localScale = Vector3.Lerp (_boss.transform.localScale, _finalScale, Time.deltaTime * 10f);
	
		} else {
			SetStatus (TaskStatus.Success);
		}
	}

}
