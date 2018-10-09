using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	public Vector3 lastMp;

	public Boid boidPrefab;
	public List<Boid> boids;

	[HideInInspector]
	public float minSpeed = 5;

	[Range(5f, 20.0f)]
	public float maxSpeed;
	public Text maxSpeedTxt;

	[Range(5f, 20.0f)]
	public float maxForce;
	public Text maxForceTxt;

	[Range(1f, 10.0f)]
	public float neighbourDistance;
	public Text neighbourDistanceTxt;

	[Range(1f, 5.0f)]
	public float personalDistance;
	public Text personalDistanceTxt;


	[Range(0.0f, 2.0f)]
	public float cohesionWeight;
	public Text cohesionWeightTxt;

	[Range(0.0f, 2.0f)]
	public float alignmentWeight;
	public Text alignmentWeightTxt;

	[Range(0.0f, 2.0f)]
	public float separateWeight;
	public Text separateWeightTxt;

	[Range(0.0f, 2.0f)]
	public float seekWeight;
	public Text seekWeightTxt;

	public GameObject player;

	void Awake() {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (this.gameObject);
	}

	// Use this for initialization
	void Start () {
		lastMp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		lastMp.z = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Mouse0)) {
			Vector3 mp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			mp.z = 0.0f;
			if (mp != lastMp && mp.x > -19.5f) {
				Boid b = Instantiate (boidPrefab, mp, Quaternion.identity).GetComponent<Boid> ();
				b.target = player.transform;
				boids.Add (b);
				lastMp = mp;
			}
		}
	}

	public List<Boid> getNeighbours(Boid boid){
		List<Boid> neighbours = new List<Boid> ();
		foreach (Boid b in boids) {
			if (b == boid)
				continue;
			else if (Vector3.Distance (b.transform.position, boid.transform.position) <= neighbourDistance)
				neighbours.Add (b);
		}
		return neighbours;
	}

	public List<Boid> getToCloseBoids(Boid boid){
		List<Boid> toclose = new List<Boid> ();
		foreach (Boid b in boids) {
			if (b == boid)
				continue;
			else if (Vector3.Distance (b.transform.position, boid.transform.position) <= personalDistance)
				toclose.Add (b);
		}
		return toclose;
	}

	public void changeMaxSpeed(float v) {
		maxSpeed = v;
		maxSpeedTxt.text = "Max Speed: " + maxSpeed.ToString();
	}

	public void changeMaxForce(float v) {
		maxForce = v;
		maxForceTxt.text = "Max Force: " + maxForce.ToString();
	}

	public void changeNeighDist(float v) {
		neighbourDistance = v;
		neighbourDistanceTxt.text = "Neigh. Dist: " + neighbourDistance.ToString();
	}

	public void changePersSpace(float v) {
		personalDistance = v;
		personalDistanceTxt.text = "Pers. Space: " + personalDistance.ToString();
	}

	public void changeCohesion(float v) {
		cohesionWeight = v;
		cohesionWeightTxt.text = "Cohesion: " + cohesionWeight.ToString();
	}

	public void changeAlignment(float v) {
		alignmentWeight = v;
		alignmentWeightTxt.text = "Alignment: " + alignmentWeight.ToString();
	}

	public void changeSeparate(float v) {
		separateWeight = v;
		separateWeightTxt.text = "Separate: " + separateWeight.ToString();
	}

	public void changeSeek(float v) {
		seekWeight = v;
		seekWeightTxt.text = "Seek: " + seekWeight.ToString();
	}

	public void reset() {
		SceneManager.LoadScene ("Game");
	}
}
