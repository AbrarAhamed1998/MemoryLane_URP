using Photon.Pun;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UIElements;

public class MultiplayerManager : MonoBehaviourPunCallbacks
{
	public static MultiplayerManager instance;

	public GameObject malePlayerPrefab;

	public GameObject femalePlayerPrefab;

	public GameObject MainUI;

	public Transform spawnLocation;

	public Canvas PauseScreen;

	public int playSceneIndex = 1, LobbySceneIndex = 0;
	#region Monovbehaviour Callbacks

	private void Start()
	{
		instance = this;
		if (malePlayerPrefab == null)
		{
			Debug.LogError("No playerPrefab assigned");

		}
		else
		{
			Debug.Log("Instantiating player in : " + SceneManager.GetActiveScene().name);
			if ( PlayerProperties.LocalPlayerInstance == null)
			{
				SpawnPlayer();
			}
			else
			{
				Debug.Log("Ignoring Player instantiation");
			}
		}

	}

	#endregion
	#region Photon Callbacks

	public override void OnLeftRoom()
	{
		SceneManager.LoadScene(0);
	}

	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		Debug.Log("A Player has entered the room :" + newPlayer.NickName);
		if (PhotonNetwork.IsMasterClient)
		{
			Debug.Log("The player entered as Master client : " + newPlayer.NickName);
			LoadArena(playSceneIndex);
		}
	}


	public override void OnPlayerLeftRoom(Player otherPlayer)
	{
		Debug.Log("A Player Left the Room : " + otherPlayer.NickName);
		if (PhotonNetwork.IsMasterClient)
		{
			Debug.Log("The player that left is a Master Client :" + otherPlayer.NickName);
			LoadArena(playSceneIndex);
		}
	}

	#endregion

	#region Public Methods

	public void LeaveRoom()
	{
		PhotonNetwork.LeaveRoom();
	}

	#endregion

	#region Private Methods

	void LoadArena(int index)
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			Debug.Log("Trying to Load a Level but we are NOT the Master Client");
		}
		Debug.Log("Loading Level : Room for " + PhotonNetwork.CurrentRoom.PlayerCount);
		PhotonNetwork.LoadLevel(index);
	}

	#endregion

	public void SpawnPlayer()
	{
		if(PhotonNetwork.NickName.Contains("Maddy") || PhotonNetwork.NickName.Contains("Harshita"))
		{
			PhotonNetwork.Instantiate(this.femalePlayerPrefab.name, spawnLocation.position, Quaternion.identity, 0);
			
		}
		else
		{
			PhotonNetwork.Instantiate(this.malePlayerPrefab.name, spawnLocation.position, Quaternion.identity, 0);
		}
	}



}
