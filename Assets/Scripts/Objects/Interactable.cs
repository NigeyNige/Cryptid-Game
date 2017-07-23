using UnityEngine;
using System.Collections;

public abstract class Interactable : MonoBehaviour {

	public abstract void OnInteract(GameObject Agent);
	public GameObject Icon;	
}
