using UnityEngine;
using System.Collections;

public class Ability_MakeNoise : Ability {

	public GameObject NoisePrefab;
	public float Range;


	public override void OnUse()
	{
		GameObject Noise = Instantiate(NoisePrefab, gameObject.transform.position, Quaternion.identity) as GameObject;
		Noise.GetComponent<Noise>().Range = this.Range;
	}
}
