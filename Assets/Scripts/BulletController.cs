using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	/*
	 * This script tells the bullet what to do upon impact.
	*/

	void OnCollisionEnter(Collision collision){

		//find out what the bullet hit, and get that objects health component
		GameObject hit = collision.gameObject;
		var health = hit.GetComponent<Health> ();

		//if there is a health component, deal 10 damage to it
		if (health != null) {
			health.takeDamage (10);
		}

		//destroy the bullet
		Destroy (gameObject);
	}
}
