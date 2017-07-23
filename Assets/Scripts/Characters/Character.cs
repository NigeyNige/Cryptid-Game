using UnityEngine;
using System.Collections;
using System;

using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class Character : Interactable {

	private State CurrentState;
	public Vision Vision;
	public float CurrentFear;
	public float FearThreshold_Flee;
	public float FearGrowSpeed;
	public float FearDrainSpeed;
	float Fear_Max = 100f;

	public float CURRENTSPEED;
	public float SPEED_RUN;
	public float SPEED_WALK;

	public GameObject EmptyGameObject;
	public GameObject NoisePrefab;
	public GameObject CameraFlash;
	public Transform CameraFlashSpawnPoint;

	public GameObject Ragdoll;

	private bool Incapacitated;

	// Use this for initialization
	void Start ()
	{
		SetState(new IdleState(this));
		Incapacitated = false;
	}

	void Update()
	{
		if (CurrentState == null)
			SetState(new IdleState(this));
		
		CurrentState.Tick();

		if (Vision.canSeePlayer)
			AddFear();

		if (CurrentFear > 0f)
			CurrentFear -= Time.deltaTime * FearDrainSpeed;
		if (CurrentFear > Fear_Max)
			CurrentFear = Fear_Max;
		if (CurrentFear < 0f)
			CurrentFear = 0;

	}

	public State GetState()
	{
		return CurrentState;
	}

	public void SetState(State state)
	{
		if (CurrentState != null)
			CurrentState.OnStateExit();

		CurrentState = state;
		gameObject.name = "Teen - " + state.GetType().Name;

		if (CurrentState != null)
			CurrentState.OnStateEnter();
	}

	public void SetSpeed_Run()
	{
		CURRENTSPEED = SPEED_RUN;
		GetComponent<NavMeshAgent>().speed = CURRENTSPEED;
	}

	public void SetSpeed_Walk()
	{
		CURRENTSPEED = SPEED_WALK;
		GetComponent<NavMeshAgent>().speed = CURRENTSPEED;
	}

	public GameObject OnIncapacitated()
	{
		Incapacitated = true;
		GetComponent<NavMeshAgent>().enabled = false;
		GetComponent<AICharacterControl>().enabled = false;
		GetComponent<Animator>().enabled = false;
		Vision.gameObject.SetActive(false);
		FallDown();

		Debug.Log("Incapacitated " + gameObject.name + "!");
		return Ragdoll;
	}

	public void OnRevived()
	{
		Incapacitated = false;
		GetComponent<NavMeshAgent>().enabled = true;
		GetComponent<AICharacterControl>().enabled = true;
		Vision.gameObject.SetActive(true);

		GetUp();
	}

	public void OnDeath()
	{
		GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().OnTeenDeath(IsInCryptidLair());
		gameObject.SetActive(false);
	}

	public void MakeNoise(float range)
	{
		Instantiate(NoisePrefab, transform.position, Quaternion.identity);
	}

	public void GetUp()
	{
		gameObject.SetActive(true);
		Ragdoll.SetActive(false);
	}

	public void FallDown()
	{
		//transform.Rotate(Vector3.left, 90f, Space.Self);
		Ragdoll.SetActive(true);
		Ragdoll.transform.parent = null;
		Ragdoll.GetComponent<TeenRagdoll>().Teen = gameObject;
		gameObject.SetActive(false);
	}

	public bool IsIncapacitated()
	{
		return Incapacitated;
	}

	public void AddFear()
	{
		CurrentFear += Time.deltaTime * FearGrowSpeed;
	}

	public void JumpScare(float Magnitude)
	{
		CurrentFear += Magnitude;
	}

	public bool IsInCryptidLair()
	{
		return GameObject.FindGameObjectWithTag("Lair").GetComponent<SphereCollider>().bounds.Contains(transform.position);
	}

	public override void OnInteract(GameObject Agent)
	{
		if (Agent.GetComponent<Cryptid>())	//find out what's interacting with me
		{
			if (Agent.GetComponent<Ability_Incapacitate>())	//find out what it can do
			{
				Agent.GetComponent<Ability_Incapacitate>().OnUse(gameObject);	//do it, and do the associated stuff to me in the 'ability' class
			}
		}
	}
}
