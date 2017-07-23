using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cryptid : MonoBehaviour {

	public Ability[] Abilities;
	GameController GC;

	Character TeenInFocus;
	public SphereCollider InteractionTrigger;

	public List<Interactable> InteractablesInTrigger;
	public Interactable HighlightedInteractable;

	// Use this for initialization
	void Start ()
	{
		GC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdateInteractionTrigger();

		UpdateInput();
	}

	void UpdateInput()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			FireInteract();
		}
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			Abilities[0].OnUse(gameObject);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			Abilities[1].OnUse(gameObject);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			Abilities[2].OnUse(gameObject);
		}
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			Abilities[3].OnUse(gameObject);
		}
	}

	void UpdateInteractionTrigger()
	{
		InteractablesInTrigger = new List<Interactable>();

		foreach (Interactable doodad in GC.Interactables)
		{
			if (InteractionTrigger.bounds.Contains(doodad.transform.position))
			{
				InteractablesInTrigger.Add(doodad);
			}
			else
			{
				doodad.Icon.SetActive(false);
			}
		}

		Interactable result = null;
		float dist = 999f;

		foreach (Interactable i in InteractablesInTrigger)
		{
			float testdist = Vector3.Distance(i.transform.position, transform.position);
			if (testdist < dist)
			{
				dist = testdist;
				result = i;
			}
			else
			{
				i.Icon.SetActive(false);
			}
		}

		HighlightedInteractable = result;

		if (HighlightedInteractable != null)
		{
			HighlightedInteractable.Icon.SetActive(true);
		}
	}

	void FireInteract()
	{
		if (HighlightedInteractable != null)
		{
			HighlightedInteractable.OnInteract(gameObject);
		}
	}
}
