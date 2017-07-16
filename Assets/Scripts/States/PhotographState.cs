using UnityEngine;
using System.Collections;

public class PhotographState : State {

	public Transform Target;

	public float PhotoTimeOut = 1f;
	bool Complete;

	public PhotographState (Character character, Transform Target) : base(character)
	{
		this.Target = Target;
		Complete = false;
	}

	public override void Tick()
	{

		if (PhotoTimeOut <= 0 && !Complete)
		{
			TakePhoto();
		}
		else
		{
			if (character.CurrentFear >= character.FearThreshold_Flee)
			{
				character.SetState(new FleeState(character, Target));
				return;
			}
			else
			{
				PhotoTimeOut -= Time.deltaTime;
				//play raise-camera animation
			}
		}

		if (character.Vision.canSeePlayer)
		{
			Target = character.Vision.GetPointOfInterest().transform;
		}
	}

	public override void OnStateEnter()
	{
		//Debug.Log(character.name + " entering state: Photograph.");
		character.SetSpeed_Walk();
	}

	public override void OnStateExit()
	{
		//Debug.Log(character.name + " exiting state: Photograph.");
	}

	public void TakePhoto()
	{
		//Debug.Log(character.name + " taking photo.");
		if (character.Vision.canSeePlayer)	//TODO: have a separate camera vision cone and visually communicate it to the player?
		{
			Debug.Log(character.name + " photo success!");
			GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().OnLose();
		}
		else
		{
			Debug.Log(character.name + " photo fail!");
		}

		Complete = true;

		//TODO: implement fear

		character.SetState(new InvestigateState(character, Target));
	}
}
