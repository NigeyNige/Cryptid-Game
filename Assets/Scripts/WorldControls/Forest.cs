using UnityEngine;
using System.Collections;

public class Forest : MonoBehaviour {

	public float Map_Size_X;
	public float Map_Size_Z;

	// Use this for initialization
	void Start ()
	{
		Map_Size_X = GetComponent<Terrain>().terrainData.heightmapWidth / 5.13f;
		Map_Size_Z = GetComponent<Terrain>().terrainData.heightmapHeight / 5.13f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
