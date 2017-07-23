using UnityEngine;
using System.Collections;

public class Ability_MakeNoise : Ability {

	public GameObject NoisePrefab;
	public float Range;

	public AudioClip SoundFile;

	public override void OnUse(GameObject Target)
	{
		GameObject Noise = Instantiate(NoisePrefab, gameObject.transform.position, Quaternion.identity) as GameObject;
		Noise.GetComponent<Noise>().Range = this.Range;
		GetComponent<AudioSource>().PlayOneShot(SoundFile);
	}
}
