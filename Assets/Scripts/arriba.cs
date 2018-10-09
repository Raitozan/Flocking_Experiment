using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arriba : MonoBehaviour {

	float timer;

	// Use this for initialization
	void Awake () {
		timer = 0.875f;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0.0f)
			Destroy (gameObject);
	}
}
