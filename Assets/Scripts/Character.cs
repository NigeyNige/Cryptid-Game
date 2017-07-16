using UnityEngine;
using System.Collections;
using System;

using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class Character : MonoBehaviour {

	private State CurrentState;
	public Vision Vision;
	public float CurrentFear;
	public float FearThreshold_Flee;
	public float FearDrainSpeed;
	float Fear_Max = 100f;

	public float CURRENTSPEED;
	public float SPEED_RUN;
	public float SPEED_WALK;

	public GameObject EmptyGameObject;
	public GameObject NoisePrefab;

	// Use this for initialization
	void Start ()
	{
		SetState(new IdleState(this));
	}

	void Update()
	{
		CurrentState.Tick();

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

	public void OnIncapacitated()
	{
		GetComponent<NavMeshAgent>().enabled = false;
		GetComponent<AICharacterControl>().enabled = false;
		Debug.Log("Incapacitated " + gameObject.name + "!");
	}

	public void OnRevived()
	{
		GetComponent<NavMeshAgent>().enabled = true;
		GetComponent<AICharacterControl>().enabled = true;
	}

	public void OnDeath()
	{
		GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().OnTeenDeath();
	}

	public void MakeNoise(float range)
	{
		Instantiate(NoisePrefab, transform.position, Quaternion.identity);
	}
}
