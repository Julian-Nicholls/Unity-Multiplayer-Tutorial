using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

	/*
	 * This script finds the camera object, and rotates to it
	*/

	RectTransform rt;

	void Start(){
		rt = GetComponent<RectTransform>();
	}

	void Update () {
		rt.LookAt (GameObject.Find("Camera").transform);
	}
}
