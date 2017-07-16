using UnityEngine;
using System.Collections;

using UnityStandardAssets.Characters.ThirdPerson;

public class LookForFriendsState : State {

	public Transform Destination;
	GameObject SearchTarget;
	bool NoFriendsLeft;

	float DistanceError = 4f;

	public LookForFriendsState (Character character) : base(character)
	{
		this.character = character;
	}

	public override void Tick()
	{
		if (ReachedTarget())
		{
			character.SetState(new IdleState(character));
		}
		else
		{
			NavMeshAgent agent = character.GetComponent<NavMeshAgent>();

			if (agent.pathStatus == NavMeshPathStatus.PathPartial && (agent.velocity.magnitude < 0.25f)) //change state if path is incomplete and no longer running anywhere
			{
				//this should be lookforfriends instead of idle
				character.SetState(new IdleState(character));

				//TODO: if can't flee any further, cower
			}
		}
	}

	public override void OnStateEnter()
	{
		//TODO: in some situations involving Noise, teens flicker rapidly between LookForFriends and Flee
		//maybe something to do with a teen accidentally lookforfriends-ing itself?

		Debug.Log(character.name + " entering state: LookForFriends.");

		if (GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GetTeensRemaining() == 1)
		{
			//if we're the only teen left, wander at random rather than towards friends
		}

		Character TeenToFind = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GetRandomTeen();	//may return this teen, causing teen to wander a short distance

		Vector3 SearchVector = (TeenToFind.transform.position - character.transform.position);

		Vector3 ErrorVector = new Vector3(Random.Range(-DistanceError, DistanceError), 0f, Random.Range(-DistanceError, DistanceError));

		SearchVector += ErrorVector;
		SearchVector += character.transform.position;

		Forest forest = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Forest>();

		float forestX = forest.Map_Size_X;
		float forestZ = forest.Map_Size_Z;

		if (SearchVector.x > forestX-1)
			SearchVector.x = forestX-1;
		if (SearchVector.x < 1)
			SearchVector.x = 1;

		if (SearchVector.z > forestZ-1)
			SearchVector.z = forestZ-1;
		if (SearchVector.z < 1)
			SearchVector.z = 1;

		SearchVector.y = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Terrain>().SampleHeight(SearchVector);

		SearchTarget = GameObject.Instantiate(character.EmptyGameObject, SearchVector, Quaternion.identity) as GameObject;

		SearchTarget.name = "SearchTarget";

		Destination = SearchTarget.transform;

		character.GetComponent<AICharacterControl>().SetTarget(Destination);
		character.SetSpeed_Walk();
	}

	public override void OnStateExit()
	{
		Debug.Log(character.name + " exiting state: LookForFriends.");
		character.SetSpeed_Walk();
	}

	private bool ReachedTarget()
	{
		return (Vector3.Distance(character.transform.position, Destination.position) < .75f);
	}
}
