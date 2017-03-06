using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task {

	public enum TaskStatus : byte{
		Detached,
		Pending,
		Working,
		Success,
		Fail,
		Aborted

	}
	public TaskStatus Status{ get; private set;}

	// Convenience status checking
	public bool IsDetached { get { return Status == TaskStatus.Detached; } }
	public bool IsAttached { get { return Status != TaskStatus.Detached; } }
	public bool IsPending { get { return Status == TaskStatus.Pending; } }
	public bool IsWorking { get { return Status == TaskStatus.Working; } }
	public bool IsSuccessful { get { return Status == TaskStatus.Success; } }
	public bool IsFailed { get { return Status == TaskStatus.Fail; } }
	public bool IsAborted { get { return Status == TaskStatus.Aborted; } }
	public bool IsFinished { get { return (Status == TaskStatus.Fail || Status == TaskStatus.Success || Status == TaskStatus.Aborted); } }

	internal void SetStatus(TaskStatus s){
		switch(s){
			case TaskStatus.Working:
				Init ();
				CleanUp ();
				break;
			case TaskStatus.Success:
				OnSuccess ();
				CleanUp ();
				break;
			case TaskStatus.Fail:
				OnFail ();
				CleanUp ();
				break;
			case TaskStatus.Aborted:
				OnAbort ();
				CleanUp ();
				break;
			case TaskStatus.Detached:
			case TaskStatus.Pending:
				break;
		}
	}
	protected virtual void OnSuccess() {}

	protected virtual void OnFail() {}

	protected virtual void OnAbort() {}

	protected virtual void Init() {}

	protected virtual void CleanUp() {}


}
