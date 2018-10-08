using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {

	Rigidbody2D rb;
	float maxForce;
	float maxSpeed;

	public Transform target;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D> ();

		Vector3 direction = new Vector3 (Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
		direction = direction.normalized * maxSpeed;

		rb.AddForce (direction);
	}
	
	// Update is called once per frame
	void Update () {
		seek ();
		updateStats ();
	}

	void seek() {
		Vector2 desired = target.position - transform.position;
		desired = desired.normalized * maxSpeed;

		Vector2 steer = desired - rb.velocity;
		steer = steer.normalized * maxForce;

		rb.AddForce (steer);
	}

	void updateStats() {
		maxForce = GameManager.instance.maxForce;
		maxSpeed = GameManager.instance.maxSpeed;
	}


}
