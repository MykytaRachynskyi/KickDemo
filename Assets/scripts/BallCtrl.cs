using UnityEngine;
using System.Collections;

public class BallCtrl : MonoBehaviour {
	Rigidbody ballrb;
	public float strength = 10f;
	Vector3 resetPosition;
	bool active;
	AudioSource audio;

	public GMscript gmscript;
	
	// Use this for initialization
	void Start () {
		ballrb = GetComponent<Rigidbody>();
		resetPosition = transform.position;
		active = true;
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void Kick (Vector3 flickDir, float velocity)
	{
		audio.Play();
		//Debug.Log("Received vector: "+flickDir);
		float upArc = (velocity)/(velocity+.3f);
		Vector3 direction = new Vector3 (flickDir.x,upArc, 1);
		if (active) {
			ballrb.AddForce (direction*strength, ForceMode.Impulse);
			active = false;
		}
	}

	public void Reset () {
		ballrb.velocity = Vector3.zero;
		transform.position = resetPosition;
		active = true;
	}

	void OnCollisionEnter (Collision collision) 
	{
		foreach (ContactPoint contact in collision.contacts) {
			if (collision.collider.name == "target") {
					gmscript.AddPoints(contact.point);
			}
		}
	}
}
