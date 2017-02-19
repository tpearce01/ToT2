using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SystemManager : MonoBehaviour {


	public static SystemManager i;								//Static reference

	public GameObject[] prefabs;								//List of all prefabs that may be instantiated
	List<GameObject> activeObjects = new List<GameObject>();	//All active objects controlled by this script

	void Start(){
		i = this;
	}

	void Update(){
		//Remove any objects that have been deleted
		activeObjects.RemoveAll(item => item == null);
	}

	//Instantiate an object at the specified location and add it to the list of active objects
	public void SpawnObject(int index, Vector3 location){
		activeObjects.Add(Instantiate (prefabs [index], location, Quaternion.identity) as GameObject);
	}

	//Convert enum to index and call SPawnObject
	public void SpawnObject(Prefab obj, Vector3 location){
		SpawnObject((int)obj, location);
	}

	public void SpawnCombatText(Vector3 location, int value){
		SpawnObject (Prefab.FloatingCombatText, location);
		activeObjects[activeObjects.Count-1].GetComponent<FloatingCombatText> ().Initialize (value);
	}
}
	
//Enum to easily convert prefab names to the appropriate index
public enum Prefab{
	Explodable = 0,
	Explosion = 1,
	BaseCast = 2,
	EnemySpawnCast1 = 3,
	Adds1 = 4,
	SmallExplosion = 5,
	FloatingCombatText = 6
};
