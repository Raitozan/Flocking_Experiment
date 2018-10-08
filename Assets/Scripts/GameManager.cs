using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	public Boid boidPrefab;
	public List<Boid> boids;
	public float maxForce;
	public float maxSpeed;

	public GameObject player;

	void Awake() {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (this.gameObject);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Mouse0)) {
			Vector3 mp = Input.mousePosition;
			mp = Camera.main.ScreenToWorldPoint (mp);
			mp.z = 0.0f;
			Boid b = Instantiate (boidPrefab, mp, Quaternion.identity).GetComponent<Boid>();
			b.target = player.transform;
			boids.Add (b);
		}
	}
}
