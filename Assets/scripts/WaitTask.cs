using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitTask : Task {

	public readonly float _duration; 
	float curTime = 0; 

	public WaitTask(float d){
		_duration = d;
	}           
	// Update is called once per frame
	public override void UpdateTask()
	{
		curTime += Time.deltaTime;
		if(curTime >= _duration){
			SetStatus (TaskStatus.Success);
		}
	}
}
