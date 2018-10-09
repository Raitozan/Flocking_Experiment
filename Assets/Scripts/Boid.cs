using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {

	Rigidbody2D rb;
	float maxForce;
	float minSpeed;
	float maxSpeed;

	float cohesionWeight;
	float alignmentWeight;
	float separateWeight;
	float seekWeight;

	public Transform target;

	public GameObject arriba;
	public float minTimer;
	public float maxTimer;
	float timer;

	// Use this for initialization
	void Start () {
		timer = Random.Range (minTimer, maxTimer);
		UpdateStats ();
		rb = gameObject.GetComponent<Rigidbody2D> ();
		Vector2 startDir = new Vector2 (Random.Range (-1.0f, 1.0f), Random.Range (-1.0f, 1.0f));
		rb.AddForce (startDir);
		rb.velocity = rb.velocity.normalized * minSpeed;
	}

	void FixedUpdate() {
		if (transform.position.x > 26.7f)
			transform.position = new Vector3 (-19.5f, transform.position.y, transform.position.z);
		else if (transform.position.x < -19.5f)
			transform.position = new Vector3 (26.7f, transform.position.y, transform.position.z);

		if (transform.position.y > 15f)
			transform.position = new Vector3 (transform.position.x, -15f, transform.position.z);
		else if (transform.position.y < -15f)
			transform.position = new Vector3 (transform.position.x, 15f, transform.position.z);

		timer -= Time.deltaTime;
		if (timer <= 0.0f) {
			Instantiate (arriba, this.transform);
			timer = Random.Range (minTimer, maxTimer);
		}
	}
	
	// Update is called once per frame
	void Update () {
		UpdateStats ();

		Cohesion ();
		Alignment ();
		Separate ();
		Seek ();

		transform.up = rb.velocity;

		if (rb.velocity.magnitude > maxSpeed)
			rb.velocity = rb.velocity.normalized * maxSpeed;
		else if (rb.velocity.magnitude < minSpeed)
			rb.velocity = rb.velocity.normalized * minSpeed;
	}

	void Cohesion() {
		Vector3 globalPos = new Vector3 (0, 0, 0);
		int count = 0;
		foreach (Boid b in GameManager.instance.getNeighbours(this)) {
			globalPos += b.transform.position;
			count++;
		}
		if (count >= 0) {
			globalPos /= count;
		
			Vector2 desired = globalPos - transform.position;
			desired = desired.normalized * maxSpeed;
			//Debug.DrawLine (transform.position, new Vector3(transform.position.x + desired.x*cohesionWeight, transform.position.y + desired.y*cohesionWeight, transform.position.z), Color.red);

			Vector2 steer = desired - rb.velocity;
				steer = steer.normalized * maxForce;

			rb.AddForce (steer*cohesionWeight);
		}
	}

	void Alignment() {
		Vector2 globalVel = new Vector2 (0, 0);
		int count = 0;
		foreach (Boid b in GameManager.instance.getNeighbours(this)) {
			if (b.rb != null) {
				globalVel += b.rb.velocity;
				count++;
			}
		}
		if (count >= 0) {
			globalVel /= count;

			Vector2 desired = globalVel;
			desired = desired.normalized * maxSpeed;
			//Debug.DrawLine (transform.position, new Vector3(transform.position.x + desired.x*alignmentWeight, transform.position.y + desired.y*alignmentWeight, transform.position.z), Color.yellow);

			Vector2 steer = desired - rb.velocity;
				steer = steer.normalized * maxForce;

			rb.AddForce (steer*alignmentWeight);
		}
	}

	void Separate() {
		Vector3 globalSep = new Vector3 (0, 0, 0);
		int count = 0;
		foreach (Boid b in GameManager.instance.getToCloseBoids(this)) {
			Vector3 sepDir = transform.position - b.transform.position;
			sepDir = sepDir.normalized / sepDir.magnitude;
			globalSep += sepDir;
			count++;
		}
		if (count >= 0) {
			globalSep /= count;

			Vector2 desired = globalSep;
			desired = desired.normalized * maxSpeed;
			//Debug.DrawLine (transform.position, new Vector3(transform.position.x + desired.x*separateWeight, transform.position.y + desired.y*separateWeight, transform.position.z), Color.blue);

			Vector2 steer = desired - rb.velocity;
				steer = steer.normalized * maxForce;

			rb.AddForce (steer*separateWeight);
		}
	}

	void Seek() {
		Vector2 desired = target.position - transform.position;
		desired = desired.normalized * maxSpeed;
		//Debug.DrawLine (transform.position, new Vector3(transform.position.x + desired.x*seekWeight, transform.position.y + desired.y*seekWeight, transform.position.z), Color.green);

		Vector2 steer = desired - rb.velocity;
			steer = steer.normalized * maxForce;

		rb.AddForce (steer*seekWeight);
	}

	void UpdateStats() {
		maxForce = GameManager.instance.maxForce;
		minSpeed = GameManager.instance.minSpeed;
		maxSpeed = GameManager.instance.maxSpeed;

		cohesionWeight = GameManager.instance.cohesionWeight;
		alignmentWeight = GameManager.instance.alignmentWeight;
		separateWeight = GameManager.instance.separateWeight;
		seekWeight = GameManager.instance.seekWeight;
	}


}
