using UnityEngine;
using System.Collections;

public class Cryptid : MonoBehaviour {

	public Ability[] Abilities;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdateInput();
	}

	void UpdateInput()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			Abilities[0].OnUse();
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			Abilities[1].OnUse();
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			Abilities[2].OnUse();
		}
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			Abilities[3].OnUse();
		}
	}
}
