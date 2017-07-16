using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject LoseMessage;
	public GameObject ReplayButton;

	int TeensRemaining;

	// Use this for initialization
	void Start ()
	{
		TeensRemaining = GameObject.FindGameObjectsWithTag("Teen").Length;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void OnWin()
	{

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

	public void OnTeenDeath()
	{
		TeensRemaining--;
		Debug.Log("TEEN DEAD");
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
