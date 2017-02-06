using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Enemy {
	public GameObject player;
	public AudioClip clip;

	public void Start(){
		player = GameObject.Find("Ship");
		Activate(); 
	}
	public override void Activate(){
		//define the type of enemy here
		PlaySound(clip);
	
	}
	public void Update(){
		FollowPlayer(player); 
	}
}
