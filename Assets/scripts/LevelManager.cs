using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadMenu ()
	{
		SceneManager.LoadScene("menu");
	}

	public void LoadGame()
	{
		SceneManager.LoadScene("main");
	}
}
