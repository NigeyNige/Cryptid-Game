using UnityEngine;
using System.Collections;

public class Noise : MonoBehaviour {

	public float Range;

	float TimeOut = 1f;

	// Use this for initialization
	void Start ()
	{
		GetComponent<SphereCollider>().radius = Range;
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Teen")
		{
			Character Listener = other.GetComponent<Character>();
			if (Listener.GetState() is FleeState)	//only attract teens if they're idling or looking for friends
			{
				//do nothing
			}
			else if (Listener.GetState() is IncapacitatedState)
			{
				//do nothing
			}
			else if (Listener.GetState() is PhotographState)
			{
				//do nothing
			}
			else if (Listener.GetState() is IncapacitatedState)
			{
				//do nothing
			}
			else
			{
				Listener.SetState(new InvestigateState(Listener, transform));
			}
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if (TimeOut <= 0 && GetComponent<SphereCollider>().enabled)
			GetComponent<SphereCollider>().enabled = false;
	}
}
