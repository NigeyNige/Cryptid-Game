using UnityEngine;
using System.Collections;

public class Vision : MonoBehaviour {

	public Transform eyesPosition;
	public bool canSeePlayer;

	public LayerMask playerLayerMask;

	public GameObject EmptyGameObject;
	public GameObject PointOfInterest;

	// Use this for initialization
	void Start ()
	{
		canSeePlayer = false;
	}

	void OnTriggerStay(Collider other)
	{		
		if (other.tag != "Player") //we can only see the player
			return;

		//Debug.DrawLine(eyesPosition.position, other.transform.position); TODO: don't cast a line every frame, maybe every three frames
		if (!Physics.Linecast(eyesPosition.position, other.transform.position, playerLayerMask))
		{
			//cast a line from the eyes to the player to see if there's a clear line of sight. If there is, we've spotted them!
			canSeePlayer = true;
			if (PointOfInterest == null)
			{
				PointOfInterest = Instantiate(EmptyGameObject, other.transform.position, Quaternion.identity) as GameObject;
				PointOfInterest.name = "Point of Interest";
			}
			else
				PointOfInterest.transform.position = other.transform.position;
		}
		else
		{
			canSeePlayer = false;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			canSeePlayer = false;
		}
			
	}

	public GameObject GetPointOfInterest()
	{
		if (PointOfInterest == null)
			Debug.Log("tried to get a null pointofinterest");
		
		return PointOfInterest;
	}
}
