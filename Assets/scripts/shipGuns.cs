using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipGuns : MonoBehaviour {

	public GameObject bullet; 
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Mouse0)){
			Fire ();
		}
	}
	void Fire(){
		var b = (GameObject)Instantiate (bullet, transform.position, transform.rotation);
		b.GetComponent<Rigidbody> ().velocity = new Vector3(20f, 0f, 0f);
		Destroy(b, 2.0f);
	}
}
