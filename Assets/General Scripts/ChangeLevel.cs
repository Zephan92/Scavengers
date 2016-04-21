using UnityEngine;
using System.Collections;

public class ChangeLevel : MonoBehaviour {
	public string level;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter()
	{
		Application.LoadLevel(level);
	}
}
