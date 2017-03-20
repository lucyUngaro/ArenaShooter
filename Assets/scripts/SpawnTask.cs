using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTask : Task {
	private readonly System.Action _action;
	bool canSpawn = false;
	float timePassed = 0;

	public SpawnTask (System.Action act)
	{
		_action = act; 
	}
	protected override void Init(){
		canSpawn = true;
	}
	protected override void OnSuccess(){
		canSpawn = false;
	}
	public override void UpdateTask()
	{
		timePassed += Time.deltaTime;

		if (canSpawn && timePassed >= 3f) {
			_action ();
			timePassed = 0;
		}
	}

}
