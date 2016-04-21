using UnityEngine;
using System.Collections;

public class CollectResource : MonoBehaviour 
{
	public float resourceLimit = 10f;
	public float setRespawnTime = 5f;
	public float minimumOutput = 1f;
	public float maximumOutput = 2f;
	public float resourceOutputMultiplier = 1f;
	private float clickCount;
	private float output;
	private GameObject currentObject;
	public static float resources;
	 
	// Use this for initialization
	private void Start ()  
	{
		clickCount = 0f;
		output = 0f;
		currentObject = null;
	}
	
	// Update is called once per frame
	private void Update () 
	{

	}

	public void Collect(GameObject other)
	{
		currentObject = other.transform.gameObject;
		clickCount++; 
		Debug.Log("The click count is: " + clickCount);
		
		if (resourceLimit > clickCount)
		{
			Debug.Log("Collected resource");
			output = resourceOutputMultiplier * Random.Range(minimumOutput,maximumOutput);
			resources = resources + output; 
		}
		else
		{
			Debug.Log("Exhausted the resource");
			output = resourceOutputMultiplier * Random.Range(10 * minimumOutput,10 * maximumOutput);
			Respawn();
			clickCount = 0;
			resources = resources + output;
		}
	}

	private void Respawn()
	{
		currentObject.GetComponent<Collider>().enabled = false;
		//Debug.Log("Collider off");
		currentObject.GetComponent<Renderer>().enabled = false;
		//Debug.Log("Renderer off");
		Invoke("RespawnHelper", setRespawnTime);
	}

	private void RespawnHelper()
	{
		currentObject.GetComponent<Collider>().enabled = true;
		//Debug.Log("Collider on");
		currentObject.GetComponent<Renderer>().enabled = true;
		//Debug.Log("Renderer on");
	}
}
