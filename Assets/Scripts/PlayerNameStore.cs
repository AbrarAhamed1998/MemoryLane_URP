using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameStore : MonoBehaviour
{
    public InputField NameInput;
    string defaultName;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("PlayerName"))
		{
            defaultName = PlayerPrefs.GetString("PlayerName");
            NameInput.text = defaultName;
            PhotonNetwork.NickName = defaultName;
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePlayerName(string value)
	{
        if(string.IsNullOrEmpty(value))
		{
            Debug.LogError("Name cannnot be empty");
            return;
		}
        PhotonNetwork.NickName = value;
        PlayerPrefs.SetString("PlayerName", value);
	}
}
