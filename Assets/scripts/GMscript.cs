using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GMscript : MonoBehaviour {
	bool isPressed, isFlick, passedBall;
	Vector3 previousPosition, initialPosition;
	float minFlickingVelocity = 0.1f, currentTime;
	Ray ray;
	RaycastHit hit;
	float velocity, score;

	public Camera camInitial, camTarget;
	public BallCtrl ball;
	public Transform ballTransform, cameraThreshold, targetCenter;
	public Text scoreText;

	// Use this for initialization
	void Start () {
		passedBall = false;
		camInitial.enabled = true;
		camTarget.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		//	Debug.Log("Currently Muse Cursor is at "+Input.mousePosition);
		if (Input.GetMouseButtonDown (0))
			StartFlick ();
		else if (Input.GetMouseButtonUp (0))
			EndFlick ();
		
		if (isPressed)
			UpdateFlick ();

		if (ballTransform.position.z > cameraThreshold.position.z && ballTransform.position.y > cameraThreshold.position.y && !camTarget.enabled) {
			SwitchCameras();
			currentTime = Time.time;
		} 

		if ((ballTransform.position.y < cameraThreshold.position.y || (Time.time - currentTime) > 3f) && camTarget.enabled) {
			ball.Reset();
			SwitchCameras();
		}
	}

	void StartFlick(){
		isPressed = true;
		//Debug.Log("Flick started: "+isPressed);
		previousPosition = /*cam.ScreenToWorldPoint*/(Input.mousePosition);
		initialPosition = Input.mousePosition;
	}

	void EndFlick ()
	{
		isPressed = false;
		//Debug.Log("Flick ended");
		Vector3 endPosition = Input.mousePosition;
		Vector3 flickDir = (endPosition - initialPosition).normalized;
		if(isFlick && passedBall)
			ball.Kick(flickDir, velocity);

		passedBall = false;
		isFlick = false;
	}

	void UpdateFlick ()
	{
		Vector3 newPosition = /*cam.ScreenToWorldPoint*/ (Input.mousePosition);
		//Debug.Log("Current mouse position: "+newPosition);
		velocity = (newPosition - previousPosition).magnitude * Time.deltaTime;
		//Debug.Log("Detected velocity: "+velocity);
		if (velocity > minFlickingVelocity)
			isFlick = true;
		else 
			isFlick = false;

		if (Physics.Raycast(ray, out hit))
			if (hit.collider.name == "Ball")
				passedBall = true;
		
		previousPosition = newPosition;
	}

	void SwitchCameras(){
		camInitial.enabled = !camInitial.enabled;
		camTarget.enabled = !camTarget.enabled;
	}

	public void AddPoints (Vector3 point)
	{
		if ((point - targetCenter.position).magnitude < 3f) {
			score += 50f;
			scoreText.text = "Score: " + score;
		} else if ((point - targetCenter.position).magnitude > 3f && (point - targetCenter.position).magnitude < 6f) {
			score += 30f;
			scoreText.text = "Score: " + score;
		} else if ((point - targetCenter.position).magnitude > 6f && (point - targetCenter.position).magnitude < 9f) {
			score += 10f;
			scoreText.text = "Score: " + score;
		} else if ((point - targetCenter.position).magnitude > 9f && (point - targetCenter.position).magnitude < 12f) {
			score += 5f;
			scoreText.text = "Score: " + score;
		}
	}	
}
