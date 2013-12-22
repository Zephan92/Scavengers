using UnityEngine;
using System.Collections;

public class Rotater : MonoBehaviour {
	
	public float rotateSpeed = 1f;
	public float x = 15;
	public float y = 40;
	public float z = 45;
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate (new Vector3(x, y, z) * rotateSpeed * Time.deltaTime);
	}
}
