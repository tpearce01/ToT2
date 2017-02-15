using UnityEngine;
using System.Collections;

public class BossBehavior : MonoBehaviour {

	//Static reference
	public static BossBehavior i;

	//Spawn Area
	public float xMin;
	public float xMax;
	public float yMin;
	public float yMax;

	//Explodable Hazard Vars
	float explodableSpawnTimer = 0.1f;	//Begin spawning explodables after 0.1s

	//Adds Vars
	float add1Timer = 10f;	//Begin Spawning adds after 5s

	//Phase vars
	public int phase;

	void Awake(){
		i = this;
	}

	void Start(){
		phase = 1;	//Begin at this phase
	}

	void Update(){

		gameObject.transform.Rotate (0, 45 * Time.deltaTime, 0);

		if (Player.i.health > 0) {
			switch (phase) {
			case 1:
				Phase1 ();
				break;
			case 2:
				Phase2 ();
				break;
			case 3:
				Phase3 ();
				break;
			}
		}
	}

	public void SpawnAdds1(float min, float max, int quantity){
		if (add1Timer <= 0) {
			//spawn hazard
			for (int i = 0; i < quantity; i++) {
				SystemManager.i.SpawnObject(Prefab.Adds1, new Vector3(Random.Range(xMin, xMax),2.5f,Random.Range(yMin,yMax)));
			}

			//reset timer
			add1Timer = Random.Range(min,max);
		} else {
			add1Timer -= Time.deltaTime;
		}
	}

	/*
	 * Polish notes on Explodables:
	 * - Use audio cue for spawn
	 * - Use visual cue (progress bar) for time until explosion
	 */ 

	//Spawns [quantity] explodables after [min] to [max] seconds
	public void SpawnExplodables(float min, float max, int quantity){
		if (explodableSpawnTimer <= 0) {
			//spawn hazard
			for (int i = 0; i < quantity; i++) {
				SystemManager.i.SpawnObject(Prefab.Explodable, new Vector3(Random.Range(xMin, xMax),1,Random.Range(yMin,yMax)));
			}

			//reset timer
			explodableSpawnTimer = Random.Range(min,max);
		} else {
			explodableSpawnTimer -= Time.deltaTime;
		}
	}

	public void SpawnExplodablesWeighted(float min, float max, int quantity){
		if (explodableSpawnTimer <= 0) {

			Vector3 location = new Vector3 (Random.Range (xMin, xMax), 1, Random.Range (yMin, yMax));
			Vector3 weightedLocation = Vector3.Lerp (location, Player.i.gameObject.transform.position, .3f);

			for (int i = 0; i < quantity; i++) {
				SystemManager.i.SpawnObject(Prefab.Explodable, weightedLocation);
			}

			//reset timer
			explodableSpawnTimer = Random.Range(min,max);
		} else {
			explodableSpawnTimer -= Time.deltaTime;
		}
	}

	public void SpawnExplodablesWeighted(float min, float max, int quantity, float weight){
		if (explodableSpawnTimer <= 0) {

			Vector3 location = new Vector3 (Random.Range (xMin, xMax), 1, Random.Range (yMin, yMax));
			Vector3 weightedLocation = Vector3.Lerp (location, Player.i.gameObject.transform.position, weight);

			for (int i = 0; i < quantity; i++) {
				SystemManager.i.SpawnObject(Prefab.Explodable, weightedLocation);
			}

			//reset timer
			explodableSpawnTimer = Random.Range(min,max);
		} else {
			explodableSpawnTimer -= Time.deltaTime;
		}
	}

	/*
	 * Phase 1
	 * Spawn adds slowly
	 */ 
	public void Phase1(){
		SpawnAdds1 (7, 9, 1);
	}
	/*
	 * Phase 2 
	 * Spawn Explodables consistently
	 * Spawn adds slowly
	 */ 
	public void Phase2(){
		if (Player.i.health >= 60) {
			SpawnExplodablesWeighted (0.5f, 1f, 1, 0.5f);
		}
		else{
			SpawnExplodablesWeighted (.5f, 1f, 1);	//Spawn 1 explodable after 0.5 to 1 seconds
		}
		SpawnAdds1(7, 9, 1);
	}

	/*
	 * Phase 3 
	 * Spawn Explodables quickly
	 * Spawn adds moderately
	 */ 
	public void Phase3(){
		
		SpawnExplodablesWeighted (0.3f, 0.4f, 1, .4f); //Spawn 1 explodables after 0.2 to 0.3 seconds
		SpawnAdds1(5, 7, 1);
	}


}
