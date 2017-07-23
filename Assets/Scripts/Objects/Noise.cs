using UnityEngine;
using System.Collections;

public class Noise : MonoBehaviour {

	public float Range;

	float TimeOut = 1f;
	bool Active;

	// Use this for initialization
	void Start ()
	{
		GetComponent<SphereCollider>().radius = Range;
		Active = true;
	}

	void OnTriggerStay(Collider other)
	{
		if (!Active)
		{
			return;
		}
		
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
			else if (!Listener.IsIncapacitated())
			{
				Listener.SetState(new InvestigateState(Listener, transform));
				Listener.JumpScare(5f);

				Active = false;
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
