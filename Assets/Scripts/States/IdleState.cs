using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class IdleState : State {

	public IdleState (Character character) : base(character)
	{
	}

	public override void Tick()
	{
		if (character.Vision.canSeePlayer)
		{
			//jump up the fear if the cryptid is spotted
			character.CurrentFear += 10f;
			character.SetState(new InvestigateState(character, character.Vision.PointOfInterest.transform));
		}
	}

	public override void OnStateEnter()
	{
		character.GetComponent<AICharacterControl>().SetTarget(null);
		//Debug.Log(character.name + " entering state: Idle.");
		GameObject.Destroy(character.Vision.PointOfInterest);
		character.Vision.PointOfInterest = null;
		character.SetSpeed_Walk();
	}

}
