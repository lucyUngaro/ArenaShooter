using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedy : Enemy {
	public GameObject player;
	GameObject leftWall, rightWall, upWall, downWall; 


	public void Start(){
		Activate(); 
		SetHealth(10); 
		SetSpeed (1); 
		EventManager.Instance.AddListener<EnemyKilledEvent> (OnEnemyKilled);
	
	}
	public override void Activate(){
		leftWall = GameObject.Find("walls/wallLeft");
		rightWall = GameObject.Find("walls/wallRight");
		upWall = GameObject.Find("walls/wallUp");
		downWall = GameObject.Find("walls/wallDown");
	}
	public void Update(){
		Move(leftWall.transform.position.x, rightWall.transform.position.x, upWall.transform.position.y, downWall.transform.position.y); 

	}
	public void IncreaseSpeed(int speed){
		SetSpeed (GetSpeed()+speed); 
	}
	void OnEnemyKilled(EnemyKilledEvent e){
		IncreaseSpeed (2); 
	}

}
