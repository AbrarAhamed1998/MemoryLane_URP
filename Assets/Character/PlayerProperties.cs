using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerProperties : MonoBehaviourPun
{
    public static GameObject LocalPlayerInstance;

	public string myName;

	public Vector3 relativeCamPos;

	public Camera myAssignedCamera;

	public List<GameObject> Accessories;

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
		AssignMainCamera(); //This is to prevent 2 Main Cameras from spawning in the scene
		
	}

	private void OnDisable()
	{
		UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	public void AssignMainCamera()
	{
		if(myAssignedCamera == null)
		{
			if (photonView.IsMine)
			{
				Camera.main.transform.parent = gameObject.transform;
				Camera.main.transform.SetAsLastSibling();
				Camera.main.transform.localPosition = relativeCamPos;
				Camera.main.transform.localRotation = Quaternion.identity;
				myAssignedCamera = Camera.main;
			}
		}
		/*else
		{
			if(photonView.IsMine)
			{
				Camera.main.transform.parent = gameObject.transform;
				Camera.main.transform.SetAsLastSibling();
				Camera.main.transform.localPosition = relativeCamPos;
				Camera.main.transform.localRotation = Quaternion.identity;
				Destroy(myAssignedCamera.gameObject); //Assuming the main camera at top of hirarchy is not used
				///Please fucking improve this its stupid
			}
		}*/
	}
}
