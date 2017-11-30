using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour {
	public GameObject cube;
	public Text turnGUI;
	public int rows;
	public int columns;
	public float offset;
	public List<GameObject> cubes;
	public GameObject[] players;
	private int currentPlayer;

	// Use this for initialization
	void Start () {
		currentPlayer = 0;
		CreateBoard ();
		PlacePlayersRandomly ();

	}

	void PlacePlayersRandomly() {
		for (int i = 0; i < players.Length; i++) {
			int playerPosition = Random.Range (0, cubes.Count);
			MoveToPosition (players [i], playerPosition);
		}

	}

	void CreateBoard() {
		for (int x = 0; x < rows; x++) {
			for (int z = 0; z < columns; z++) {
				Vector3 position = new Vector3 (x + (x * offset), 0, z + (z * offset));
				GameObject go = Instantiate (cube, position, transform.rotation, transform);
				cubes.Add (go);
			}
		}

	}

	void MoveToPosition(GameObject player, int position) {
		player.transform.parent = cubes [position].transform;
		player.transform.localPosition = Vector3.up;
		player.GetComponent<Player> ().currentPosition = position;
		currentPlayer++;
		if (currentPlayer >= players.Length) {
			currentPlayer = 0;
		}
		turnGUI.text = "Current Player: " + (currentPlayer + 1).ToString ();
	}
		

	void MovePlayer() {
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			if (players[currentPlayer].GetComponent<Player>().currentPosition + 1 < cubes.Count && players[currentPlayer].GetComponent<Player>().currentPosition%columns != columns - 1) {
				MoveToPosition (players [currentPlayer], players [currentPlayer].GetComponent<Player> ().currentPosition + 1);
			}
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			if (players[currentPlayer].GetComponent<Player>().currentPosition > 0 && players[currentPlayer].GetComponent<Player>().currentPosition%columns != 0) {
				MoveToPosition (players [currentPlayer], players [currentPlayer].GetComponent<Player> ().currentPosition - 1);
			}
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			if (players[currentPlayer].GetComponent<Player>().currentPosition - columns >= 0) {
				MoveToPosition (players [currentPlayer], players [currentPlayer].GetComponent<Player> ().currentPosition - columns);
			}
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			if (players[currentPlayer].GetComponent<Player>().currentPosition + columns < cubes.Count) {
				MoveToPosition (players [currentPlayer], players [currentPlayer].GetComponent<Player> ().currentPosition + columns);
			}
		}

	}
	// Update is called once per frame
	void Update () {
		MovePlayer ();		
	}
}
