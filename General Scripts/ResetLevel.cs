using UnityEngine;
using System.Collections;

public class ResetLevel : MonoBehaviour {
	private GenerateTerrain gt;
	public GameObject go;
	public GameObject[] destroy;

	void Start()
	{
		gt = go.GetComponent("GenerateTerrain") as GenerateTerrain;
	}

	public void ResetMap(GameObject other)
	{
		Debug.Log(GenerateTerrain.blocksGenerated + " blocks were created");
		destroy = GameObject.FindGameObjectsWithTag("ResetMap");
		for(int i = 0; i < GenerateTerrain.blocksGenerated; i++)
		{Destroy(destroy[i], 0f);}
		for(int i = 0; i < 7; i++)
		{
			for(int j = 0; j < 7; j++)
			{
				if (i == 6 || j == 6){GenerateTerrain.alreadySpawned[i,j] = true;}
				else {GenerateTerrain.alreadySpawned[i,j] = false;}
			}
		}
		gt.BuildStartArea();
		GenerateTerrain.forksInTheRoad = 0f;
		GenerateTerrain.endPossible = true;
		GenerateTerrain.blocksGenerated = 0f;
	}
}
