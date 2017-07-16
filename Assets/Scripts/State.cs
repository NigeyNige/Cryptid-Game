using UnityEngine;
using System.Collections;

public abstract class State {

	protected Character character;

	public abstract void Tick();

	public virtual void OnStateEnter(){}
	public virtual void OnStateExit(){}

	public State(Character character)
	{
		this.character = character;
	}

}
