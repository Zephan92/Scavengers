using UnityEngine;
using System.Collections;

public class DoNotDestroyOnSceneChange : MonoBehaviour {
	public GameObject objectToKeep;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(objectToKeep);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
