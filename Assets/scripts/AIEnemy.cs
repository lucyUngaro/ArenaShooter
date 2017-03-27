using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemy : Enemy {
	
	public GameObject player; 

	//private AIEnemy.State state; 

	private FSM<AIEnemy> _fsm;


	//limits of space
	public GameObject wallLeft, wallRight, wallUp, wallDown; 

	public int lerpAmount;
	public int lerpSpeed;
	private float timePassed;
	private int totalPulses;
	private int lerpTimes;
	private bool crRunning; 
	private int curHealth;

	public void Start(){
		_fsm = new FSM<AIEnemy> (this);
		_fsm.TransitionTo<Seeking>();

		wallLeft = GameObject.Find("walls/wallLeft");
		wallRight = GameObject.Find("walls/wallRight");
		wallUp = GameObject.Find("walls/wallUp");
		wallDown = GameObject.Find("walls/wallDown");

		Activate ();
	}

	public override void Activate ()
	{
		player = GameObject.Find("Ship");
		lerpAmount = 2;
		SetHealth (20);
		curHealth = 20;
		timePassed = 0f;
		lerpSpeed = 2;
		totalPulses = 0;
		lerpTimes = 5;
		crRunning = false;

	}

	public Vector3 PickPosition(){
		return PickRandomPosition (wallLeft.transform.position.x, wallRight.transform.position.x, wallUp.transform.position.y, 
			wallDown.transform.position.y);
	}
	public void Update(){
		_fsm.Update ();
	}


	void Pulse(){
		if (!crRunning && totalPulses < lerpTimes) {
			totalPulses++;
			StartCoroutine (LerpScale ());
		}
	}
	IEnumerator LerpScale(){
		if (crRunning) yield break; 
		crRunning = true; 
		yield return new WaitForSeconds (0.5f);
		Vector3 initialScale = transform.localScale;
		while(transform.localScale != initialScale * lerpAmount){
			transform.localScale = Vector3.Lerp(transform.localScale, initialScale * lerpAmount, timePassed);
			timePassed += Time.deltaTime * lerpSpeed;
			yield return null;
		
		}
		timePassed = 0;
		yield return new WaitForSeconds (0.5f);
		while (transform.localScale != initialScale) {
			transform.localScale = Vector3.Lerp(transform.localScale, initialScale, timePassed);
			timePassed += Time.deltaTime * lerpSpeed;
			yield return null;
		}
		timePassed = 0;
		crRunning = false;
	}

	private class AIEnemyState : FSM<AIEnemy>.State {


		protected float GetDistance(Vector3 g)
		{

			return Vector3.Distance (Context.transform.position, g);

		}

		protected void TravelToPoint(Vector3 pt){
			Vector3 directionToPlayer = pt - Context.transform.position; 
			Context.transform.GetComponent<Rigidbody>().AddForce(directionToPlayer.normalized);

		}

	}
	private class Seeking : AIEnemyState{
		public override void Update(){
			TravelToPoint (Context.player.transform.position);

			if (GetDistance (Context.player.transform.position) <= 2f) {

				TransitionTo<Preparing> (); 
				Context.curHealth = Context.GetHealth (); 
				Context.GetComponent<Rigidbody> ().velocity = Vector3.zero; 
			}
		}
	}
	private class Preparing : AIEnemyState{

		public override void Update(){
			Context.Pulse (); 
			if (Context.totalPulses < Context.lerpTimes && Context.curHealth > Context.GetHealth ()) {
				Context.StopCoroutine (Context.LerpScale ());
				TransitionTo<Fleeing> (); 
			} else if (Context.totalPulses >= Context.lerpTimes) {
				TransitionTo<Attacking>(); 
			}
		}


	}
	private class Fleeing : AIEnemyState{
		Vector3 pos;
		public override void OnEnter(){
			pos = Context.PickPosition ();
			TravelToPoint (pos);

		}
		public override void Update(){
			if (GetDistance (pos) <= 2f) {
				Context.transform.GetComponent<Rigidbody> ().velocity = Vector3.zero;
				TransitionTo<Seeking> ();
			}
		}

	}
	private class Attacking : AIEnemyState{

		public override void OnEnter(){

			Context.totalPulses = 0; 

		}

		public override void Update(){
			TravelToPoint (Context.player.transform.position);
			if(GetDistance(Context.player.transform.position) <= 2f){
				bool hitPlayer = Context.Explode (Context.player, 2f);
				if (hitPlayer) Context.SetHealth (0);
				TransitionTo<Seeking>(); 
			}

		}

	}
}
