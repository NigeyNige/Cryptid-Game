using UnityEngine;
using System.Collections;

public class Ability_Incapacitate : Ability {

	public float Range;
	public float AttackForce;
	public GameObject EffectPrefab;

	public AudioClip SoundFile;
	public Vector3 AttackVector;

	public override void OnUse(GameObject Target)
	{
		AttackVector = transform.TransformDirection(Vector3.forward);

		Character TargetTeen = Target.GetComponent<Character>();

		GameObject ragdoll = TargetTeen.OnIncapacitated();
		ragdoll.GetComponentInChildren<Rigidbody>().AddForce(AttackVector * AttackForce, ForceMode.Impulse);

		GetComponent<AudioSource>().PlayOneShot(SoundFile);
		Instantiate(EffectPrefab, transform.position + AttackVector, Quaternion.identity);

		/*	Old code pre-interactsphere, uses raycasts to hit

		Vector3 Origin = transform.position + new Vector3(0f, 1f, 0f);
		RaycastHit hit;

		if (Physics.Raycast(Origin, AttackVector, out hit, Range))
		{
			if (hit.collider.gameObject.tag == "Teen")
			{
				if (hit.collider.gameObject.GetComponent<Character>().IsIncapacitated())
					return;
				GameObject ragdoll = hit.collider.gameObject.GetComponent<Character>().OnIncapacitated();
				GetComponent<AudioSource>().PlayOneShot(SoundFile);
				ragdoll.GetComponentInChildren<Rigidbody>().AddForce(AttackVector * AttackForce, ForceMode.Impulse);
				Instantiate(EffectPrefab, hit.point, Quaternion.identity);
			}
		}

		//Debug.DrawRay(Origin, AttackVector, Color.red, 1f, false);

		*/

	}
}
