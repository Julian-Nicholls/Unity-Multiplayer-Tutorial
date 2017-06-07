using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking; 

public class Health : NetworkBehaviour {

	/*
	 * This script tracks the player's health and ensures this information is synced across clients.
	*/

	public const int maxHealth = 100;

	//curentHealth variable is synced
	[SyncVar(hook = "onHealthChange")]
	public int currentHealth = maxHealth;

	public RectTransform healthBar;
	public bool destroyOnDeath; 
	private NetworkStartPosition[] spawnPoints; 

	void Start(){
		if (isLocalPlayer) {

			//find possible spawnpoints for future use
			spawnPoints = FindObjectsOfType<NetworkStartPosition> ();
		}
		
	}

	public void takeDamage(int amount){

		//damage is only taken on the server
		if (!isServer) {
			return;
		}

		currentHealth = currentHealth - amount;

		//if they dead
		if (currentHealth <= 0) {

			//if the object is supposed to be destroyed on death or respawned
			if (destroyOnDeath) {
				Destroy (gameObject);
			} else {
				currentHealth = maxHealth;
				RpcRespawn ();
			}
		}
	}

	//"hook", executed when currentHealth changes
	void onHealthChange(int currHealth){

		//change the look of the healthbar
		healthBar.sizeDelta = new Vector2 (currHealth, healthBar.sizeDelta.y);
	}

	//called on server, executed on client
	[ClientRpc]

	//respawns player using saved array of points
	void RpcRespawn(){
		if (isLocalPlayer) {

			//start with empty vector3
			Vector3 spawnPoint = Vector3.zero;

			//unless the array is null or empty, choose one at random
			if (spawnPoints != null && spawnPoints.Length > 0) {
				spawnPoint = spawnPoints [Random.Range (0, spawnPoints.Length)].transform.position; 
			}

			//set transform to new spawn position
			transform.position = spawnPoint;
		}
	}

}
