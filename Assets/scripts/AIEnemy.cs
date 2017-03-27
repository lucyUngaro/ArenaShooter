using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemy : Enemy {
	
	public GameObject player; 

	private AIEnemy.State state; 

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
		wallLeft = GameObject.Find("walls/wallLeft");
		wallRight = GameObject.Find("walls/wallRight");
		wallUp = GameObject.Find("walls/wallUp");
		wallDown = GameObject.Find("walls/wallDown");

		Activate ();
	}
	public enum State {
		Seeking,
		Preparing,
		Attacking,
		Fleeing
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


	public void Update(){
		switch (state) {
		case State.Seeking:
			TravelToPoint (player.transform.position);
			if (GetDistance(player) <= 2f) {
				curHealth = GetHealth ();
				state = State.Preparing;
				transform.GetComponent<Rigidbody> ().velocity = Vector3.zero; 
			}
			break;
		case State.Preparing:
			Invoke ("Pulse", 1);
			if (totalPulses < lerpTimes && curHealth > GetHealth ()) {
				state = State.Fleeing; 
				StopCoroutine (LerpScale ());
			} else if (totalPulses >= lerpTimes) {
				state = State.Attacking; 
			}
			break; 
		case State.Attacking:
			TravelToPoint (player.transform.position);
			if (GetDistance (player) <= 2f) {
				bool hitPlayer = Explode (player, 2f);
				if (hitPlayer) SetHealth (0);
				state = State.Seeking; 
			}
			break; 
		case State.Fleeing:
			Vector3 pos = PickRandomPosition (wallLeft.transform.position.x, wallRight.transform.position.x, wallUp.transform.position.y, 
				              wallDown.transform.position.y);
			TravelToPoint (pos);
			if (transform.position == pos) {
				state = State.Seeking; 
			}
			break; 
			
		default:
			break;
		}
	}
	float GetDistance(GameObject g)
	{
		
		return Vector3.Distance (transform.position, g.transform.position);

	}
	void Pulse(){
		if (!crRunning && totalPulses < lerpTimes) {
			totalPulses++;
			StartCoroutine (LerpScale ());
		}
	}
	IEnumerator LerpScale(){
		crRunning = true; 
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
		crRunning = false;
	}
}
