using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject LoseMessage;
	public GameObject ReplayButton;

	public List<Interactable> Interactables;
	public GameObject[] Teens;
	int TeensRemaining;
	public int Score;

	// Use this for initialization
	void Start ()
	{
		Teens = GameObject.FindGameObjectsWithTag("Teen");
		Interactables = new List<Interactable>();

		foreach (Interactable i in GameObject.FindObjectsOfType<Interactable>())
			Interactables.Add(i);
		TeensRemaining = Teens.Length;
		Score = 0;

	}
	
	// Update is called once per frame
	void Update ()
	{
		Interactables = new List<Interactable>();

		foreach (Interactable i in GameObject.FindObjectsOfType<Interactable>())
			Interactables.Add(i);
	}

	public void OnWin()
	{
		Debug.Log("WIN! SCORE OF " + Score);
	}

	public void OnLose()
	{
		LoseMessage.SetActive(true);
		ReplayButton.SetActive(true);
	}

	public void Restart()
	{
		SceneManager.LoadScene(0);
	}

	public void OnTeenDeath(bool DiedInLair)
	{
		Teens = GameObject.FindGameObjectsWithTag("Teen");
		TeensRemaining = Teens.Length;
		Debug.Log("TEEN DEAD");

		if (DiedInLair)
			Score += 10;
		else
			Score += 5;

		Debug.Log("CURRENT SCORE " + Score);

		if (TeensRemaining == 0)
			OnWin();
	}

	public int GetTeensRemaining()
	{
		return TeensRemaining;
	}

	public Character GetRandomTeen()
	{
		GameObject[] teens = GameObject.FindGameObjectsWithTag("Teen");
		return teens[Random.Range(0, teens.Length-1)].GetComponent<Character>();
	}

	public Character GetRandomTeen(Character NotThisOne)
	{
		GameObject[] teens = GameObject.FindGameObjectsWithTag("Teen");

		if (teens.Length < 2)
			return teens[0].GetComponent<Character>();

		GameObject[] teensExcludingThatOne = new GameObject[teens.Length-1];

		for (int i = 0; i < teens.Length; i++)
		{
			if (teens[i] != NotThisOne.gameObject)
			{
				//TODO: this is out of bounds sometimes - why? in the meantime just don't use this method, let the teens occasionally wander stupidly
				teensExcludingThatOne[i] = teens[i];
			}
		}

		GameObject result = teensExcludingThatOne[Random.Range(0, teensExcludingThatOne.Length-1)];

		Debug.Log("returning " + result);

		return result.GetComponent<Character>();
	}
}
