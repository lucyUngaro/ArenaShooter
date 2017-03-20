using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManagr 
{
	private readonly List<Task> _tasks = new List<Task>();


	public void AttachTask(Task t)
	{
		if (t != null & !t.IsAttached) 
		{
			_tasks.Add (t); 
			t.SetStatus (Task.TaskStatus.Pending); 
	
		}
	}
	public void Update()
	{
		// go thru tasks starting from most recently added
		// if they have just been added, initialize them
		// if a task finishes during initialization, complete it
		// clear it if it's done
		for (int i = _tasks.Count-1; i >= 0; i --)
		{
			//if the task was just attached it's pending
			if(_tasks[i].IsPending)
			{
				_tasks[i].SetStatus(Task.TaskStatus.Working); 
			
			}
			//it's also possible that the task is already finished
			if (_tasks [i].IsFinished) 
			{

				//start the next task if this one was successful
				if (_tasks [i].NextTask != null) 
				{
					Task newTask = _tasks [i].NextTask;
					AttachTask (newTask);
				}
				//then get rid of the old task
				_tasks[i].SetStatus(Task.TaskStatus.Detached); 
				_tasks.RemoveAt(i);
			
			}
		}
	}
}
