using UnityEngine;
using System.Collections;

public class TerrainTrigger : MonoBehaviour {
	public GameObject thisTrigger;
	public Transform blockPosition;
	public GameObject go;
	private bool isGenerated;
	private string tagName;
	private GenerateTerrain gt;
	private Vector3 positionOfTrigger;

	void Start() 
	{
		gt = go.GetComponent("GenerateTerrain") as GenerateTerrain;
		isGenerated = false;
		thisTrigger.renderer.enabled = true;
		tagName = thisTrigger.transform.gameObject.tag;
		//Debug.Log("Spawned a " + tagName);
	}

	public void OnTriggerEnter()
	{//Debug.Log("Entered a " + tagName);
		if(isGenerated != true)
		{
			thisTrigger.renderer.enabled = false;
			if(tagName == "NorthTrigger")
			{//Debug.Log("Started to Build North Terrain");
				//Debug.Log("This trigger is at " + blockPosition.position.ToString());
				positionOfTrigger = blockPosition.position;
				gt.BuildNorthTerrain(positionOfTrigger);
				isGenerated = true;
			}
			else if(tagName == "EastTrigger")
			{//Debug.Log("Started to Build East Terrain");
				//Debug.Log("This trigger is at " + blockPosition.position.ToString());
				positionOfTrigger = blockPosition.position;
				gt.BuildEastTerrain(positionOfTrigger);
				isGenerated = true;
			}
			else{Debug.Log("Trigger Tag did not match: " + tagName);}
		}
	}
}
