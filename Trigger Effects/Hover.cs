using UnityEngine;
using System.Collections;

public class Hover : MonoBehaviour 
{
	public float hover = 1;
	
	void OnTriggerStay(Collider other) 
	{
       	other.rigidbody.AddForce(Vector3.up * hover, ForceMode.Acceleration);
    }
}
