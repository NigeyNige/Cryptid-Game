using UnityEngine;
using System.Collections;

using UnityStandardAssets.Characters.ThirdPerson;
public class FleeState : State {
	
	public Transform FleeingTo;
	public Transform FleeingFrom;
	float distanceToFlee = 35f;
	GameObject FleeTarget;

	public FleeState (Character character, Transform SpookyThing) : base(character)
	{
		this.FleeingFrom = SpookyThing;
		this.character = character;
	}

	public override void Tick()
	{
		if (ReachedTarget())
		{
			//this should be lookforfriends instead of idle
			character.SetState(new LookForFriendsState(character));
		}
		else
		{
			NavMeshAgent agent = character.GetComponent<NavMeshAgent>();

			if (agent.pathStatus == NavMeshPathStatus.PathPartial && (agent.velocity.magnitude < 0.25f)) //change state if path is incomplete and no longer running anywhere
			{
				//this should be lookforfriends instead of idle
				character.SetState(new LookForFriendsState(character));
				character.AddFear();

				//TODO: if can't flee any further, cower
			}
		}
	}

	public override void OnStateEnter()
	{
		//Debug.Log(character.name + " entering state: Flee.");

		Vector3 FleeingToVector = character.transform.position - ((FleeingFrom.position - character.transform.position).normalized) * distanceToFlee;

		Forest forest = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Forest>();

		float forestX = forest.Map_Size_X;
		float forestZ = forest.Map_Size_Z;

		if (FleeingToVector.x > forestX-1)
			FleeingToVector.x = forestX-1;
		if (FleeingToVector.x < 1)
			FleeingToVector.x = 1;

		if (FleeingToVector.z > forestZ-1)
			FleeingToVector.z = forestZ-1;
		if (FleeingToVector.z < 1)
			FleeingToVector.z = 1;

		FleeingToVector.y = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Terrain>().SampleHeight(FleeingToVector);

		FleeTarget = GameObject.Instantiate(character.EmptyGameObject, FleeingToVector, Quaternion.identity) as GameObject;

		FleeTarget.name = "FleeTarget";

		FleeingTo = FleeTarget.transform;

		character.GetComponent<AICharacterControl>().SetTarget(FleeingTo);
		character.SetSpeed_Run();
	}

	public override void OnStateExit()
	{
		//Debug.Log(character.name + " exiting state: Flee.");
		character.SetSpeed_Walk();
		GameObject.Destroy(FleeTarget);
	}

	private bool ReachedTarget()
	{
		return (Vector3.Distance(character.transform.position, FleeingTo.position) < .75f);
	}
}
