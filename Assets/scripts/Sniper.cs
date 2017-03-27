using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Enemy {
	public GameObject player;
	public AudioClip clip;


	public void Start(){
		player = GameObject.Find("Ship");
		Activate(); 
		SetHealth(3); 
	}
	public override void Activate(){
		PlaySound(clip);
	
	}
	public void Update(){
		TravelToPoint(player.transform.position); 
	
	}

}
