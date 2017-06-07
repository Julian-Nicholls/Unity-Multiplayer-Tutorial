using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour {

	/* This script generates a number of enemies when the server starts
	*/

	public GameObject enemyPrefab;
	public int numberOfEnemies;

	public override void OnStartServer(){

		//create a specific number of baddies
		for (int i = 0; i < numberOfEnemies; i++) {

			//generate a random position
			Vector3 spawnPosition = new Vector3 (
				                        Random.Range (-8.0f, 8.0f),
				                        0.0f,
				                        Random.Range (-8.0f, 8.0f));

			//generate a random rotation
			Quaternion spawnRotation = Quaternion.Euler (
				                           0.0f,
				                           Random.Range (0, 180),
				                           0.0f);

			//instantiate and then spawn (admittedly not sure why it has to be instantiated first...)
			GameObject enemy = (GameObject)Instantiate (enemyPrefab, spawnPosition, spawnRotation);
			NetworkServer.Spawn (enemy); 
		}
	}
}
