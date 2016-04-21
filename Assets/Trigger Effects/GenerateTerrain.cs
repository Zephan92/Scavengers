using UnityEngine;
using System.Collections;

public class GenerateTerrain : MonoBehaviour
{	
	/// <Possible Block Outcomes>
	/// Each possible block, The name convention is:   entranceToExitXX_XX
	public GameObject southToDeadEnd10_10;
	public GameObject southToEast10_10;
	public GameObject southToEnd10_10;
	public GameObject southToNorth10_10;
	public GameObject southToNorth_East10_10;
	public GameObject startToNorth_East10_10;
	public GameObject westToDeadEnd10_10;
	public GameObject westToEast10_10;
	public GameObject westToEnd10_10;
	public GameObject westToNorth10_10;
	public GameObject westToNorth_East10_10;
	/// <Possible Block Outcomes>


	/// <Placeholder GameObjects>
	/// block is used for instantiation.
	/// createBlock is used as a variable.
	private GameObject block;
	private GameObject createBlock;
	/// <Placeholder GameObjects>


	/// <Variables>
	/// x and z are used with the alreadySpawned array.
	/// rangeMax is used for max random block switch case paths.
	/// randomBlockNumber and randomBlockNumberString are used to hold random values in use in the switch/case.
	/// currentPosition holds the position of the block currently trying to generate to.
	private int x;
	private int z;
	private int rangeMax = 4;
	private int randomBlockNumber;
	private string randomBlockNumberString;
	private Vector3 currentPosition;
	/// <Variables>


	/// <Static Variables>
	/// These are used as counters and checks.
	/// forksInTheRoad is a counter for use in ends/deadends. If forksInTheRoad is == 0, dead ends cannot appear.
	/// alreadySpawned is an array that holds whether or not a block space is going to be used or not.
	/// endPossible is a check to see if the end has been spawned or not.
	/// blocksGenerated is a counter of how many blocks are currently spawned.
	public static float forksInTheRoad;
	public static bool[,] alreadySpawned = new bool[7,7];
	public static bool endPossible;
	public static float blocksGenerated;
	/// <Static Variables>


	/// <Start>
	void Start()
	{//Debug.Log("Start");
		for(int i = 0; i < 7; i++)///This loop is used to reset the array to all false, except for the outer rim.
		{for(int j = 0; j < 7; j++)///The outer rim is set to true, so no blocks can be generated there.
			{if (i == 6 || j == 6){alreadySpawned[i,j] = true;}
			 else {alreadySpawned[i,j] = false;}}}
		block = null;
		createBlock = null;
		endPossible = true;
		blocksGenerated = 0f;
		forksInTheRoad = 0f;
		currentPosition = new Vector3(0,0,0);
		BuildStartArea();///Builds the start area
	}/// <Start>


	/// <Build Start Area>
	public void BuildStartArea()
	{//Debug.Log("Build Start Area");
		createBlock = startToNorth_East10_10;
		block = Instantiate(createBlock, new Vector3(0,0,0),  new Quaternion(0,0,0,0)) as GameObject;
		block.tag = "ResetMap";///Tagged to destroy later.
		alreadySpawned[0,0] = true;
		blocksGenerated++;
		forksInTheRoad = 1f;
	}/// <Build Start Area>


	/// <Build North Terrain>
	public void BuildNorthTerrain(Vector3 positionOfTrigger)
	{//Debug.Log("Build North Terrain");
		currentPosition = new Vector3(0,0,10) + positionOfTrigger;
		x = (int) (currentPosition.x / 10);
		z = (int) (currentPosition.z / 10);
		if(currentPosition == new Vector3(50,0,50) && endPossible == true)///Spawn an end
		{//Debug.Log("Spawning end");
			endPossible = false;
			createBlock = southToEnd10_10;
			forksInTheRoad--;
		}
		else if (alreadySpawned[x,z+1] == true && alreadySpawned[x+1,z] == true)///Spawn a dead end
		{///Debug.Log("Spawning a dead end");		
			if (forksInTheRoad == 0 && endPossible == true)
			{//Debug.Log("No more paths, spawning end.");
				createBlock = southToEnd10_10;
				forksInTheRoad--;
				endPossible = false;
			}
			else
			{	createBlock = southToDeadEnd10_10;
				forksInTheRoad--;
			}
		}
		else if (currentPosition.x == 50)///Spawn north path
		{//Debug.Log("Spawning a north path");
			createBlock = southToNorth10_10;
			alreadySpawned[x,z+1] = true;
		}
		else if (currentPosition.z == 50 || alreadySpawned[x,z+1] == true)///Spawn a corner
		{//Debug.Log("Spawning a corner");
			createBlock = southToEast10_10;
			alreadySpawned[x+1,z] = true;
		}
		else///Spawn a random block
		{//Debug.Log("Spawning random block");
			randomBlockNumber = Random.Range(0, rangeMax);
			randomBlockNumberString = randomBlockNumber.ToString("f0");
			//Debug.Log("Random number was = " + randomBlockNumberString);
			switch(randomBlockNumberString)///Spawn a random block
			{
			case "0":///Spawn a north path
				createBlock = southToNorth10_10;
				alreadySpawned[x,z+1] = true;
				break;

			case "1"://Spawn a corner
				if (alreadySpawned[x+1,z] == true)//Spawn a corner
				{
					createBlock = southToNorth10_10;
					alreadySpawned[x,z+1] = true;
				}
				else//already something there, spawn north path.
				{
					createBlock = southToEast10_10;
					alreadySpawned[x+1,z] = true;
				}
				break;

			case "2"://Spawn a dead end.
				if (forksInTheRoad == 0 && endPossible == true)//Only this path left, spawn an end.
				{//Debug.Log("No more paths, spawning end.");
					//Debug.Log("Forks in the road left: " + forksInTheRoad);
					createBlock = southToEnd10_10;
					forksInTheRoad--;
					endPossible = false;
				}
				else if (blocksGenerated >= 5)//Spawn a dead end.
				{//Debug.Log("Dead End");
					createBlock = southToDeadEnd10_10;
					forksInTheRoad--;
				}
				else//Not far enough from start, spawning a north path.
				{
					createBlock = southToNorth10_10;
					alreadySpawned[x,z+1] = true;
				}
				break;

			case "3"://Spawn a fork in the road
				if(alreadySpawned[x+1,z] == true || alreadySpawned[x,z+1] == true)//Not enough room, spawn north path
				{
					createBlock = southToNorth10_10;
				}
				else//Spawn a fork in the road
				{
					createBlock = southToNorth_East10_10;
					forksInTheRoad++;
					alreadySpawned[x+1,z] = true;
					alreadySpawned[x,z+1] = true;
				}
				break;

			case "4"://Spawn an end -Very rare-.
				createBlock = southToEnd10_10;
				forksInTheRoad--;
				endPossible = false;
				break;

			default://Spawn default north path.
				createBlock = southToNorth10_10;
				alreadySpawned[x,z+1] = true;
				break;
			}
		}
		//Debug.Log("Instantiating north block at " + currentPosition.ToString());
		block = Instantiate(createBlock, currentPosition, new Quaternion(0,0,0,0)) as GameObject;
		block.tag = "ResetMap";
		x = (int) currentPosition.x / 10;
		z = (int) currentPosition.z / 10;
		alreadySpawned[x,z] = true;
		blocksGenerated++;
	}/// <Build North Terrain>


	/// <Build East Terrain>
	public void BuildEastTerrain(Vector3 positionOfTrigger)
	{//Debug.Log("Build East Terrain");
		currentPosition = new Vector3(10,0,0) + positionOfTrigger;
		x = (int) (currentPosition.x / 10);
		z = (int) (currentPosition.z / 10);
		if(currentPosition == new Vector3(50,0,50) && endPossible == true)//Spawn an end
		{//Debug.Log("Spawning end");
			endPossible = false;
			createBlock = westToEnd10_10;
			forksInTheRoad--;
		}
		else if (alreadySpawned[x+1,z] == true && alreadySpawned[x,z+1] == true)//Spawn a dead end
		{//Debug.Log("Spawning a dead end");
			if (forksInTheRoad == 0 && endPossible == true)//Spawn an end if no more paths are possible
			{//Debug.Log("No more paths, spawning end.");
				createBlock = westToEnd10_10;
				forksInTheRoad--;
				endPossible = false;
			}
			else//Spawn a dead end
			{
				createBlock = westToDeadEnd10_10;
				forksInTheRoad--;
			}

		}
		else if (currentPosition.z == 50)//Spawn an east path
		{//Debug.Log("Spawning an east path");
			createBlock = westToEast10_10;//Spawn an east path if something is in the way.
			alreadySpawned[x+1,z] = true;
		}
		else if (currentPosition.x == 50 || alreadySpawned[x+1,z] == true)//Spawn a corner
		{//Debug.Log("Spawning a corner");
			createBlock = westToNorth10_10;
			alreadySpawned[x,z+1] = true; 
		}  
		else//Spawn a random block
		{//Debug.Log("Spawning random block");
			randomBlockNumber = Random.Range(0, rangeMax);
			randomBlockNumberString = randomBlockNumber.ToString("f0");
			//Debug.Log("Random number was = " + randomBlockNumberString);
			switch(randomBlockNumberString)//Spawn a random block
			{
			case "0"://Spawn an east path
				createBlock = westToEast10_10;
				alreadySpawned[x+1,z] = true;
				break;
				
			case "1"://Spawn a corner
				if (alreadySpawned[x,z+1] == true)//Spawn an east path if something is in the way
				{
					createBlock = westToEast10_10;
					alreadySpawned[x+1,z] = true;
				}
				else//Spawn a corner
				{
					createBlock = westToNorth10_10;
					alreadySpawned[x,z+1] = true;
				}
				break;

			case "2"://Spawn a dead end
				if (forksInTheRoad == 0 && endPossible == true)//If no more paths are possible spawn an end
				{//Debug.Log("No more paths, spawning end.");
					//Debug.Log("Forks in the road left: " + forksInTheRoad);
					createBlock = westToEnd10_10;
					forksInTheRoad--;
					endPossible = false;
				}
				else if (blocksGenerated >= 5)//Spawn a dead end
				{//Debug.Log("Dead End");
					createBlock = westToDeadEnd10_10;
					forksInTheRoad--;
				}
				else//Spawn an east path if the block is not far enough away from start
				{
					createBlock = westToEast10_10;
					alreadySpawned[x+1,z] = true;
				}
				break;

			case "3"://Spawn a fork in the road
				if (alreadySpawned[x+1,z] == true || alreadySpawned[x,z+1] == true)//Spawn an east path if taken already
				{
					createBlock = westToEast10_10;
				}
				else//Spawn a fork in the road
				{
					createBlock = westToNorth_East10_10;
					forksInTheRoad++;
					alreadySpawned[x+1,z] = true;
					alreadySpawned[x,z+1] = true;
				}
				break;
				
			case "4"://Spawn an end -Very Rare-
				createBlock = westToEnd10_10;
				forksInTheRoad--;
				endPossible = false;
				break;
				
			default://Spawn an east path
				createBlock = westToEast10_10;
				alreadySpawned[x+1,z] = true;
				break;
			}
		}
		//Debug.Log("Instantiating east block at " + currentPosition.ToString());
		block = Instantiate(createBlock, currentPosition, new Quaternion(0,0,0,0)) as GameObject;
		block.tag = "ResetMap";
		x = (int) currentPosition.x / 10;
		z = (int) currentPosition.z / 10;
		alreadySpawned[x,z] = true;
		blocksGenerated++;
	}/// <Build East Terrain>

}