using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManagr {
	private readonly List<Task> _tasks = new List<Task>();


	public void AttachTask(Task t){
		if (t != null & !t.IsAttached) {
			_tasks.Add (t); 
			t.SetStatus (Task.TaskStatus.Pending); 
		}
	}
}
