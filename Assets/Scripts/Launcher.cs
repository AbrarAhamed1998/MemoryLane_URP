using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks
{
    #region Private Serializable Fields




    #endregion



    #region Private Fields

    string gameVersion = "1";

	bool isConnecting;

	[SerializeField]
	GameObject progressLabel;
	[SerializeField]
	GameObject controlPanel;
	#endregion

	#region Public Fields

	public byte maxNumberOfPlayers;

	#endregion


	#region Monobehaviour Callbacks

	private void Awake()
	{
        PhotonNetwork.AutomaticallySyncScene = true;
    }

	// Start is called before the first frame update
	void Start()
    {
		
    }
	

	#endregion


	#region Public Methods

	public void Connect()
	{
		controlPanel.SetActive(false);
		progressLabel.SetActive(true);
		if(PhotonNetwork.IsConnected)
		{
			
			PhotonNetwork.JoinRandomRoom();
		}
		else
		{
			isConnecting = PhotonNetwork.ConnectUsingSettings();
			PhotonNetwork.GameVersion = gameVersion;
		}
	}


	#endregion



	#region Monobehaviour PUn callbacks

	public override void OnConnectedToMaster()
	{
		Debug.Log("OnConnectedMaster() was called by PUN");
		if(isConnecting)
		{
			PhotonNetwork.JoinRandomRoom();
			isConnecting = false;
		}
		
	}


	public override void OnDisconnected(DisconnectCause cause)
	{
		Debug.LogWarning("OnDisconnected() was called with cause" + cause);
		progressLabel.SetActive(false);
		controlPanel.SetActive(true);
		isConnecting = false;
	}

	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		Debug.Log("OnJoinRandomRoomFailed() is called");
		PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxNumberOfPlayers });
	}

	public override void OnJoinedRoom()
	{
		Debug.Log("OnJoinedRoom() is called");
		if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
		{
			Debug.Log("Loading Level : Room for " + PhotonNetwork.CurrentRoom.PlayerCount);

			PhotonNetwork.LoadLevel(1);
		}
	}


	#endregion

}
