using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTask : Task {

	private readonly System.Action _action; 

	public ActionTask (System.Action act)
	{
		_action = act; 
	}

	protected override void Init()
	{
		SetStatus (TaskStatus.Success);
		_action (); 
	
	}

}
