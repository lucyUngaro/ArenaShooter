using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : Enemy {
	GameObject leftWall, rightWall, upWall, downWall; 
	public AudioClip clip; 
	public void Start(){
		Activate(); 
		leftWall = GameObject.Find("walls/wallLeft");
		rightWall = GameObject.Find("walls/wallRight");
		upWall = GameObject.Find("walls/wallUp");
		downWall = GameObject.Find("walls/wallDown");
		//start teleporting
		StartCoroutine(move()); 
	}
	public override void Activate(){
		//define the type of enemy here
		SetHealth(1); 
		PlaySound(clip);
	
	}
	IEnumerator move(){
		while(true){
			Teleport(leftWall.transform.position.x, rightWall.transform.position.x, upWall.transform.position.y, downWall.transform.position.y);
			yield return new WaitForSeconds(1f); 
		}
	}
}
