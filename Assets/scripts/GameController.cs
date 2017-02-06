using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	List<Enemy> enemies = new List<Enemy>(); 
	public GameObject wallLeft;
	public GameObject wallRight; 
	public GameObject wallUp;
	public GameObject wallDown; 
	public Sniper sniperPrefab; 
	public Teleporter teleporterPrefab; 

	void Start () {
		wallLeft = GameObject.Find("walls/wallLeft");
		wallRight = GameObject.Find("walls/wallRight");
		wallUp = GameObject.Find("walls/wallUp");
		wallDown = GameObject.Find("walls/wallDown");
		StartCoroutine("createEnemy");
	}

	void _createEnemy(Enemy enemy){
		Vector3 location = new Vector3(Random.Range(wallLeft.transform.position.x, wallRight.transform.position.x), 
		Random.Range (wallDown.transform.position.y, wallUp.transform.position.y), 0);
		Enemy newEnemy = (Enemy)Instantiate (enemy, location, Quaternion.identity);
	}
	
	IEnumerator createEnemy(){
	   while(true) {
          _createEnemy(sniperPrefab);
          yield return new WaitForSeconds(10f);
		  _createEnemy(teleporterPrefab);
	   }
	}
}
