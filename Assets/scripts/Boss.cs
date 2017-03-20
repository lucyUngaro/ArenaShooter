using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy {
	public int FullHealth;
	// Use this for initialization
	void Start () {
		Activate ();
	}
	
	public override void Activate(){
		SetHealth (10);
		FullHealth = 10;
	}
	public int Health(){
		return GetHealth ();
	}

}

