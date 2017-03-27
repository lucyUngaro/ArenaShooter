using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {
	
	public abstract void Activate();
	private int health; 
	private int speed;


	//manager
	private EnemyManager _manager;
    public void SetManager(EnemyManager manager) {
        _manager = manager;
    }

	//toys for the sandbox:
	protected void SetHealth(int num){
		health = num; 
		if(GetHealth()<=0){	
			_manager.DestroyEnemy(this, this.gameObject);
			EventManager.Instance.Fire(new EnemyKilledEvent()); 
		}
	}
	protected int GetHealth(){
		return health; 
	}
	protected void SetSpeed(int num){
		speed = num; 
	}
	protected int GetSpeed(){
		return speed; 
	}
	protected void Teleport(float leftLimit, float rightLimit, float upperLimit, float lowerLimit){
		Vector3 newLocation = new Vector3(Random.Range(leftLimit, rightLimit), Random.Range(lowerLimit, upperLimit), 0);
		transform.position = newLocation; 
	}
	protected Vector3 PickRandomPosition(float leftLimit, float rightLimit, float upperLimit, float lowerLimit){
		return new Vector3(Random.Range(leftLimit, rightLimit), Random.Range(lowerLimit, upperLimit), 0);
	}
	protected void TravelToPoint(Vector3 pos){
		Vector3 directionToPlayer = pos - transform.position; 
		GetComponent<Rigidbody>().AddForce(directionToPlayer.normalized);
	}
	protected void Move(float leftLimit, float rightLimit, float upperLimit, float lowerLimit){
		Vector3 ranDirection = new Vector3 (Random.Range (leftLimit, rightLimit), Random.Range (lowerLimit, upperLimit), 0); 
		Vector3 directionToPoint = ranDirection - transform.position; 
		GetComponent<Rigidbody>().AddForce(directionToPoint.normalized*speed); 
	}
	protected void PlaySound(AudioClip enemySound){
		 AudioSource audio = GetComponent<AudioSource>();
		 audio.clip = enemySound; 
		 audio.Play(); 
	}
	protected bool Explode(GameObject player, float radius){
		if(Vector3.Distance(transform.position, player.transform.position) <= radius){
			player.GetComponent<PlayerController>().TakeDamage(9);
			return true;
		}
		return false; 
	}
	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "bullet"){
			health = GetHealth(); 
			SetHealth(health-1);
			if(GetHealth()<=0){	
				_manager.DestroyEnemy(this, this.gameObject);
				EventManager.Instance.Fire(new EnemyKilledEvent()); 
			}
		}
	}


}
