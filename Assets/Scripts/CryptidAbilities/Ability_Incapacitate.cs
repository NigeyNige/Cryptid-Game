using UnityEngine;
using System.Collections;

public class Ability_Incapacitate : Ability {

	public float Range;

	public override void OnUse()
	{
		Vector3 Origin = transform.position + new Vector3(0f, 1f, 0f);
		Vector3 AttackVector = transform.TransformDirection(Vector3.forward);
		RaycastHit hit;

		if (Physics.Raycast(Origin, AttackVector, out hit, Range))
		{
			if (hit.collider.gameObject.tag == "Teen")
			{
				hit.collider.gameObject.GetComponent<Character>().OnIncapacitated();
			}
		}

		Debug.DrawRay(Origin, AttackVector, Color.red, 1f, false);
	}
}
