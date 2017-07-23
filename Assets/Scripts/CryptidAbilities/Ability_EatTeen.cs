using UnityEngine;
using System.Collections;

public class Ability_EatTeen : Ability {

	public float Range;

	public AudioClip SoundFile;

	public override void OnUse(GameObject Target)
	{
		Vector3 Origin = transform.position + new Vector3(0f, -.1f, 0f);
		Vector3 AttackVector = transform.TransformDirection(Vector3.forward);
		RaycastHit hit;

		if (Physics.Raycast(Origin, AttackVector, out hit, Range))
		{
			if (hit.collider.gameObject.tag == "Ragdoll")
			{
				Debug.Log("EAT");
				hit.collider.transform.root.GetComponent<TeenRagdoll>().Teen.GetComponent<Character>().OnDeath();
				hit.collider.transform.root.gameObject.SetActive(false);
				GetComponent<AudioSource>().PlayOneShot(SoundFile);
			}
		}

		Debug.DrawRay(Origin, AttackVector, Color.red, 1f, false);
	}
}
