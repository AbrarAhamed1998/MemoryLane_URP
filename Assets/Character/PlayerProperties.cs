using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerProperties : MonoBehaviourPun
{
    public static GameObject LocalPlayerInstance;

	public string myName;

	public Vector3 relativeCamPos;

	private void Awake()
	{
		if(photonView.IsMine)
		{
            LocalPlayerInstance = this.gameObject;
		}
        DontDestroyOnLoad(this.gameObject);
	}
	private void Start()
	{
		myName = PlayerPrefs.GetString("PlayerName");
		AssignMainCamera();
	}
	private void OnEnable()
	{
		UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadingMode)
	{
		this.CalledOnLevelWasLoaded(scene.buildIndex);
	}

	void CalledOnLevelWasLoaded(int level)
	{
		// check if we are outside the Arena and if it's the case, spawn around the center of the arena in a safe zone
		if (!Physics.Raycast(transform.position, Vector3.down, 5f))
		{
			transform.position = new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z);
		}
	}

	private void OnDisable()
	{
		UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	public void AssignMainCamera()
	{
		if (photonView.IsMine)
		{
			Camera.main.transform.parent = gameObject.transform;
			Camera.main.transform.SetAsLastSibling();
			Camera.main.transform.localPosition = relativeCamPos;
			Camera.main.transform.localRotation = Quaternion.identity;
		}
	}
}
