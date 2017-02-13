﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public GameObject bullet; 
	public GameObject ship; 
	string lastMoved;
	void Start(){
		ship = GameObject.Find("Ship");
	}
	// Update is called once per frame
	void Update () {
		Move ();
		if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space)){
			Shoot ();
		}
		if(Input.GetKeyDown(KeyCode.A)){
			lastMoved = "A";
		}
		else if(Input.GetKeyDown(KeyCode.D)){
			lastMoved = "D";
		}
	
	}
	void Move(){
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f;
		var y = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

		ship.transform.Translate(x, 0, 0);			
		ship.transform.Translate(0, y, 0);
	}
	void Shoot(){ 
		var b = (GameObject)Instantiate (bullet, ship.transform.position , ship.transform.rotation);
		//ignore collision
		Physics.IgnoreCollision(b.GetComponent<Collider>(), ship.GetComponent<Collider>());
		Vector2 cursorInWorldPos = Camera.main.ScreenToWorldPoint( Input.mousePosition );
		Vector2 direction = cursorInWorldPos - (Vector2)ship.transform.position;
		b.GetComponent<Rigidbody> ().velocity = direction;
		Destroy(b, 1.0f);
	}
}
