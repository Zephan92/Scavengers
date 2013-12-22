using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	private static int count;
	public GUIText countText;
	public GUIText winText;
	public GUIText pauseText;
	public GUIText resourceCountText;
	public float speed;
	private Vector3 movement;
	public float winCondition = 12;
	private bool paused = false;
	private CollectResource resource;
	private ResetLevel reset;
	private GameObject resetObject;
	private ResetLevel resetReference;
	public static bool canTeleport;
	public Transform player;
	public string level;

		
	void Start ()
	{//Debug.Log("Player Start");
		Application.LoadLevel(level);
		setResourceCountText();
		count = 0;
		setCountText();
		winText.text = "";
		pauseText.text = "";
		resource = null;
		CollectResource.resources = 0;
	}

	CollectResource CheckForResource() 
	{
		RaycastHit hit;
		
		if ( Physics.Raycast( Camera.main.ScreenPointToRay( Input.mousePosition ), out hit, 1 << 9 ) ) 
		{
			return hit.transform.gameObject.GetComponent<CollectResource>();
		} 
		else 
		{
			return null;
		}
	}

	ResetLevel CheckForReset() 
	{
		RaycastHit hit;
		
		if ( Physics.Raycast( Camera.main.ScreenPointToRay( Input.mousePosition ), out hit, 1 << 9 ) ) 
		{
			return hit.transform.gameObject.GetComponent<ResetLevel>();
		} 
		else 
		{
			return null;
		}
	}

	void Update()
	{
		player.rigidbody.isKinematic = false;
		if ( Input.GetMouseButtonUp( 0 ) ) 
		{		
			resource = CheckForResource();
			reset = CheckForReset();
			if (resource != null)
			{//Debug.Log("Click on Resource");

				string tag = resource.transform.gameObject.tag;
				if (tag == "Resource")
				{
					resource.Collect(resource.transform.gameObject);
					setResourceCountText();
				}

			}
			if (reset != null)
			{Debug.Log("Clicked on Reset");
				resetObject = reset.transform.gameObject;
				resetReference = reset;
				Invoke("destroyMapDelay", 3f);
			}
		}

		if (Input.GetKey(KeyCode.Escape))
		{
			Debug.Log("Quiting");
			Application.Quit();
		}
		
		if (Input.GetKeyDown(KeyCode.KeypadEnter))
		{
			Debug.Log("Pressed Enter");
			paused = !paused;
			if (paused == true)
			{
				Debug.Log("Pausing");
				pauseText.text = "PAUSED";
				Time.timeScale = 0;
			}
			else if (paused == false)
			{
				Debug.Log("The enter button isn't pressed");
				pauseText.text = "";
				Time.timeScale = 1;
			}
		}
	}
	
	void FixedUpdate ()
	{	
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		
		movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		
		rigidbody.AddForce(movement * speed * Time.deltaTime);
	}
	
	void OnTriggerEnter(Collider other) 
	{
       	if(other.gameObject.tag == "PickUp")
		{
			other.gameObject.SetActive(false);
			count++;
			setCountText();
		}
    }
	
	void setCountText()
	{
		countText.text = "Count: " + count.ToString();
		if(count >= winCondition)
		{
			winText.text = "YOU WIN";

		}
	}

	public void setResourceCountText()
	{ 
		resourceCountText.text = "Resources: " + CollectResource.resources.ToString("f0");
	}

	public void destroyMapDelay()
	{
		resetReference.ResetMap(resetObject);
		player.position = new Vector3(5,0.5f,5);
		player.rigidbody.isKinematic = true;
	}
}