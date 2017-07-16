using UnityEngine;
using System.Collections;

using UnityStandardAssets.Characters.ThirdPerson;

public class IncapacitatedState : State {

	float TimeIncapacitated;

	public IncapacitatedState (Character character) : base(character)
	{
	}

	public override void Tick()
	{
		TimeIncapacitated += Time.deltaTime;

		if (Mathf.Floor(TimeIncapacitated) % 5 == 0)	//make a noise every five seconds
		{
			character.MakeNoise(5f);
		}
	}

	public override void OnStateEnter()
	{
		TimeIncapacitated = 0f;
		character.GetComponent<AICharacterControl>().SetTarget(null);
		Debug.Log(character.name + " entering state: Incapacitated.");
		character.Vision.PointOfInterest = null;
		character.OnIncapacitated();
	}

	public override void OnStateExit()
	{
		character.OnRevived();
	}

}
