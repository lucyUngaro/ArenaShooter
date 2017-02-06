using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {
	//this is what the subclasses will need to override
	public abstract void Activate();
	
	//toys for the sandbox:
	protected void Teleport(float leftLimit, float rightLimit, float upperLimit, float lowerLimit){
		Vector3 newLocation = new Vector3(Random.Range(leftLimit, rightLimit), Random.Range(lowerLimit, upperLimit), 0);
		transform.position = newLocation; 
	}
	protected void FollowPlayer(GameObject player){
		Vector3 directionToPlayer = player.transform.position - transform.position; 
		GetComponent<Rigidbody>().AddForce(directionToPlayer.normalized);
	}
	protected void PlaySound(AudioClip enemySound){
		 AudioSource audio = GetComponent<AudioSource>();
		 audio.clip = enemySound; 
		 audio.Play(); 
	}
}
