using UnityEngine;
using System.Collections;

public class Ability_DragTeen : Ability {

	public float Range;

	public AudioClip SoundFile;

	GameObject TeenBeingDragged;
	public Transform HoldBodyPoint;
	public Transform DropBodyPoint;

	public override void OnUse(GameObject Target)
	{
		if (TeenBeingDragged != null)
		{
			DropTeen();
			return;
		}

		Vector3 Origin = transform.position + new Vector3(0f, -.15f, 0f);
		Vector3 AttackVector = transform.TransformDirection(Vector3.forward);
		RaycastHit hit;

		if (Physics.Raycast(Origin, AttackVector, out hit, Range))
		{
			if (hit.collider.transform.root.tag == "Ragdoll")
			{
				if (TeenBeingDragged != hit.collider.transform.root.gameObject)
				{
					//if we haven't grabbed it, grab it
					GrabTeen(hit.collider.transform.root.gameObject);
				}
			}
		}

		Debug.DrawRay(Origin, AttackVector, Color.red, 1f, false);
	}

	public void DropTeen()
	{
		if (TeenBeingDragged != null)
		{
			TeenBeingDragged.GetComponent<TeenRagdoll>().DropBit();
			GetComponent<AudioSource>().PlayOneShot(SoundFile);

			TeenBeingDragged = null;
		}
	}

	public void GrabTeen(GameObject TeenToGrab)
	{
		Rigidbody BitGrabbed = TeenToGrab.GetComponent<TeenRagdoll>().GrabRandomBit(HoldBodyPoint);
		TeenBeingDragged = BitGrabbed.gameObject.transform.root.gameObject;
		GetComponent<AudioSource>().PlayOneShot(SoundFile);
	}
}
