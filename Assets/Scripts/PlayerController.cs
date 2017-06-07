using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	/*
	 * This script is in charge of controlling player action. Responsibilities include ensuring the player character
	 * is the right colour, firing bullets (via the server), and moving the player's avatar.
	*/

	public GameObject bulletPrefab;
	public Transform bulletSpawn;

	public override void OnStartLocalPlayer()
	{
		//player controls the blue player object
		GetComponent<MeshRenderer>().material.color = Color.blue;
	}

	
	void CmdFire ()
	{

		//create from bullet prefab
		GameObject bullet = (GameObject)Instantiate (
			                    bulletPrefab,
			                    bulletSpawn.position,
			                    bulletSpawn.rotation);

		//add velocity
		bullet.GetComponent<Rigidbody> ().velocity = bullet.transform.forward * 6;

		//fire over the network
		NetworkServer.Spawn (bullet);

		//destroy bullet after 2 seconds
		Destroy (bullet, 2.0f);

	}

	
	// Update is called once per frame
	void Update () {

		//only run this function for the localPlayer
		if (!isLocalPlayer) {
			return;
		}

		//check for firing command
		if (Input.GetKeyDown (KeyCode.Space)) {
			CmdFire ();
		}

		//get movement / rotation input
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

		//apply movement / rotation input
		transform.Rotate(0, x, 0);
		transform.Translate(0, 0, z);
	}
}
