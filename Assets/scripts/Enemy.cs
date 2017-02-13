using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {
	
	public abstract void Activate();
	private int health; 

	//manager
	private GameController _manager;
    public void SetManager(GameController manager) {
        _manager = manager;
    }

	//toys for the sandbox:
	protected void SetHealth(int num){
		health = num; 
	}
	protected int GetHealth(){
		return health; 
	}
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
	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "bullet"){
			health = GetHealth(); 
			SetHealth(health-1);
			if(GetHealth()<=0){
				_manager.DestroyEnemy(this, this.gameObject);
			}
		}
	}
	

}
