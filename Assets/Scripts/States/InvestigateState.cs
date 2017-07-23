using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class InvestigateState : State {

	public Transform PlaceOfInterest;

	public float InvestigationDuration = 5f;
	public float PhotoDelay = 1f;

	float InvestigateTimeOut;
	float PhotoCountDown;

	public InvestigateState (Character character, Transform PlaceOfInterest) : base(character)
	{
		this.PlaceOfInterest = PlaceOfInterest;
		InvestigateTimeOut = InvestigationDuration;
		PhotoCountDown = PhotoDelay;
	}

	public override void Tick()
	{

		if (ReachedPlaceOfInterest())
		{
			if (InvestigateTimeOut <= 0)
			{
				character.SetState(new IdleState(character));
			}
			else
			{
				InvestigateTimeOut -= Time.deltaTime;
				character.AddFear();
				//what to do while standing there having reached the suspicious area?
			}
		}


		if (character.Vision.canSeePlayer)
		{
			//tick fear up if the cryptid's in view
			character.AddFear();

			//if the fear is above the fear threshold, flee!
			if (character.CurrentFear > character.FearThreshold_Flee)
			{
				character.SetState(new FleeState(character, PlaceOfInterest));
				GameObject.Destroy(character.Vision.PointOfInterest);
				return;
			}

			//update the PlaceOfInterest if the cryptid's in view
			PlaceOfInterest = character.Vision.GetPointOfInterest().transform;

			//get ready to take a photo
			PhotoCountDown -= Time.deltaTime;

			//Debug.Log(character.name + " getting camera ready!");

			if (PhotoCountDown <= 0)
			{
				//take a photo!
				character.SetState(new PhotographState(character, PlaceOfInterest));
				character.GetComponent<AICharacterControl>().SetTarget(null);
			}
		}
		else
		{
			PhotoCountDown = PhotoDelay;
			//Debug.Log(character.name + " missed it. Putting camera away.");
			character.GetComponent<AICharacterControl>().SetTarget(PlaceOfInterest);
		}
	}

	public override void OnStateEnter()
	{
		//Debug.Log(character.name + " entering state: Investigate.");
		character.GetComponent<AICharacterControl>().SetTarget(PlaceOfInterest);
		character.SetSpeed_Walk();
	}

	public override void OnStateExit()
	{
		//Debug.Log(character.name + " leaving state: Investigate.");
	}

	private bool ReachedPlaceOfInterest()
	{
		return (Vector3.Distance(character.transform.position, PlaceOfInterest.position) < .75f);
	}
}
