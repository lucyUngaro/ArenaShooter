using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSequence : MonoBehaviour {
	TaskManagr tm = new TaskManagr();
	EnemyManager em;
	Boss boss;
	//Make Boss visible
	ActionTask ShowBoss;
	//Boss scales down
	ScaleTask ScaleBoss1;
	//Wait
	WaitTask Wait1;
	//Boss scales up to 100%
	ScaleTask ScaleBoss2; 
	//Spawn waves until boss is 50% health
	SpawnTask SpawnEnemies;
	//Fires until reaches 15% health

	//Chases player

	// Use this for initialization
	void Start () { 
		Vector3 initialScale = transform.localScale;
		em = GameObject.Find("GameController").GetComponent<EnemyManager>();
		boss = em.boss;
		//all tasks
		ScaleBoss1 = new ScaleTask (this.gameObject, initialScale*.1f);
		ScaleBoss2 = new ScaleTask(this.gameObject, initialScale); 
		ShowBoss = new ActionTask (() => {this.GetComponent<Renderer> ().enabled = true;});
		Wait1 = new WaitTask (1f);
		SpawnEnemies = new SpawnTask (() => {em.CreateRandomEnemy (new Vector3(transform.position.x, transform.position.y, 0));});

		//put tasks in order
		SequenceActions ();

	}


	void SequenceActions ()
	{
		ScaleBoss1
			.Then(ShowBoss)
			.Then(Wait1)
			.Then(ScaleBoss2)
			.Then(SpawnEnemies);
		tm.AttachTask (ScaleBoss1);  
	}

	void Update()
	{
		tm.Update ();
		if (ScaleBoss2.IsWorking) {
			ScaleBoss2.ScaleObj ();
		}
		if (ScaleBoss1.IsWorking) {
			ScaleBoss1.ScaleObj ();
		}
		if (Wait1.IsWorking) {
			Wait1.UpdateTime ();
		}
		if (SpawnEnemies.IsWorking) {
			SpawnEnemies.SpawnObject ();
			if (boss.Health() <= boss.FullHealth/2) {
				SpawnEnemies.SetStatus (Task.TaskStatus.Success);
			}
		}
		
	}
}
