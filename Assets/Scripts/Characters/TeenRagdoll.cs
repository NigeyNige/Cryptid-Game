using UnityEngine;
using System.Collections;

public class TeenRagdoll : Interactable {

	public Rigidbody[] GrabbableBits;
	public Rigidbody BitGrabbed;

	Transform SavedParent;
	FixedJoint j;
	public GameObject Teen;

	// Use this for initialization
	void Start ()
	{
		GrabbableBits = GetComponentsInChildren<Rigidbody>();
		foreach(Rigidbody rb in GrabbableBits)
		{
			rb.drag = 1f;
		}
	}

	public override void OnInteract(GameObject Agent)
	{
		if (Agent.GetComponent<Cryptid>())
		{
			if (BitGrabbed != null)
				DropBit();
			else
				GrabRandomBit(Agent.GetComponent<Ability_DragTeen>().HoldBodyPoint);
		}
	}

	public Rigidbody GrabRandomBit(Transform GrabberPoint)
	{
		int index = Random.Range(0, GrabbableBits.Length);

		BitGrabbed = GrabbableBits[index];

		j = BitGrabbed.gameObject.AddComponent<FixedJoint>();
		j.connectedBody = GrabberPoint.GetComponent<Rigidbody>();
		j.gameObject.transform.position = GrabberPoint.position;
		BitGrabbed.GetComponent<Collider>().enabled = false;
		foreach(Collider c in GetComponentsInChildren<Collider>())
			c.enabled = false;
		j.autoConfigureConnectedAnchor = true;
		Debug.Log("grabbed " + BitGrabbed.name);
		return BitGrabbed;
		Icon.SetActive(false);
	}

	void FixedUpdate()
	{
		Teen.transform.position = transform.position;
	}

	public void DropBit()
	{
		j.connectedBody = null;
		Destroy(j);
		foreach(Collider c in GetComponentsInChildren<Collider>())
			c.enabled = true;
		BitGrabbed = null;
	}
}
