using System.Collections;
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
		int num = lastMoved == "A" ? -2 : 2;
		int vel =  lastMoved == "A" ? -40 : 40;
		var b = (GameObject)Instantiate (bullet, ship.transform.position + new Vector3(num, 0, 0), ship.transform.rotation);
		//ignore collision
		Physics.IgnoreCollision(b.GetComponent<Collider>(), ship.GetComponent<Collider>());
		b.GetComponent<Rigidbody> ().velocity = new Vector3(vel, 0f, 0f);
		Destroy(b, 1.0f);
	}
}
