using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
	//list of enemies
	List<Enemy> enemies = new List<Enemy>(); 

	//limits of space
	public GameObject wallLeft, wallRight, wallUp, wallDown; 

	//enemy prefabs
	public Sniper sniperPrefab; 
	public Teleporter teleporterPrefab; 
	public Speedy speedyPrefab; 
	public Boss bossPrefab;
	public AIEnemy aiPrefab;

	//Boss
	public Boss boss;
	Enemy prevEnemy; 
	//vars
	int curWave = 1;
	bool waveComplete = true;

	void Start () {
		GetLimits(); 
	}
	public void Update(){
		if(waveComplete){
			switch(curWave){
				case 1:
					Invoke("WaveFive", 1); 
					waveComplete = false; 
					break; 
				case 2:
					Invoke("WaveTwo", 1);
					waveComplete = false; 
					break;
				case 3: 
					Invoke("WaveThree", 1);
					waveComplete=false; 
					break;
				case 4:
					Invoke("WaveFour", 1);
					waveComplete = false;
					break;
				case 5:
					Invoke("WaveFour", 1);
					waveComplete = false;
					break;
				default: 
					break; 
			}
		}
	
	}
	
	//get limits of space
	private void GetLimits(){
		wallLeft = GameObject.Find("walls/wallLeft");
		wallRight = GameObject.Find("walls/wallRight");
		wallUp = GameObject.Find("walls/wallUp");
		wallDown = GameObject.Find("walls/wallDown");
	}
	//create enemy
	public void CreateEnemy(Enemy enemy, Vector3 location){
		Enemy newEnemy = (Enemy)Instantiate (enemy, location, Quaternion.identity);
		enemies.Add(newEnemy); 
		newEnemy.SetManager(this);
		if(prevEnemy!=null) Physics.IgnoreCollision (newEnemy.GetComponent<Collider> (), prevEnemy.GetComponent<Collider> ()); 
		prevEnemy = newEnemy; 
	}
	//destroy enemy
	public void DestroyEnemy(Enemy enemy, GameObject g){
		enemies.Remove(enemy); 
		Destroy(g); 
		if(enemies.Count==0){
			waveComplete=true;
			curWave++; 
		}
	}
	//random enemy type
	public void CreateRandomEnemy(Vector3 loc){
		int num = (int) Random.Range (0f, 3f);
	
		if (num == 0) {
			CreateEnemy (sniperPrefab, loc);
		} else if (num == 1) {
			CreateEnemy (teleporterPrefab, loc);
		} else {
			CreateEnemy (speedyPrefab, loc);
		}
	
	}
	//first wave
	public void WaveOne(){
		CreateEnemy(teleporterPrefab, RandomizeLocation()); 
		CreateEnemy(sniperPrefab, RandomizeLocation()); 
	}
	//second wave
	public void WaveTwo(){
		for(int i = 0; i < 3; i++){
			Vector3 loc = RandomizeLocation(); 
			CreateEnemy(sniperPrefab, loc); 
		}
		CreateEnemy(teleporterPrefab, RandomizeLocation()); 
		CreateEnemy (speedyPrefab, RandomizeLocation ()); 
	}
	//third wave
	public void WaveThree(){
		Vector3 loc = new Vector3(wallRight.transform.position.x-2, 0, 0);
		for(int i = 0; i < 3; i++){
			loc += new Vector3(-5, 0, 0); 
			CreateEnemy(sniperPrefab, loc); 
			CreateEnemy(teleporterPrefab, RandomizeLocation()); 
		}

	}
	//fourth wave
	public void WaveFour(){ 
		CreateEnemy(bossPrefab, Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, 0f)));
		boss = (Boss) enemies [0];
		boss.transform.position = new Vector3 (boss.transform.position.x, boss.transform.position.y, 0);
		boss.GetComponent<Renderer> ().enabled = false;
	}
	//fifth wave
	public void WaveFive(){
		CreateEnemy (aiPrefab, RandomizeLocation ());

	}
	public Vector3 RandomizeLocation(){
		Vector3 loc = new Vector3(Random.Range(wallLeft.transform.position.x, wallRight.transform.position.x), 
		Random.Range (wallDown.transform.position.y, wallUp.transform.position.y), 0);
		return loc; 
	}
}
